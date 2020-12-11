//******************************************************************************************************
//  SubscriberHandler.cs - Gbtc
//
//  Copyright © 2019, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
//  file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  06/23/2019 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using sttp;
using System;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace CaptureMetadata
{
    public class SubscriberHandler : SubscriberInstance
    {
        private readonly string m_name;

        private static readonly object s_consoleLock = new object();

        public SubscriberHandler(string name) => m_name = name;

        protected override void StatusMessage(string message)
        {
            // Calls can come from multiple threads, so we impose a simple lock before write to console
            lock (s_consoleLock)
                Console.WriteLine($"[{m_name}] {message}\n");
        }

        protected override void ErrorMessage(string message)
        {
            // Calls can come from multiple threads, so we impose a simple lock before write to console
            lock (s_consoleLock)
                Console.Error.WriteLine($"[{m_name}] {message}\n");
        }

        protected override void ReceivedMetadata(ByteBuffer payload)
        {
            StatusMessage($"Received {payload.Count} bytes of metadata, parsing...");
            
            //base.ReceivedMetadata(payload);

            byte[] payloadBytes = payload.ToArray();

            if (MetadataCompressed)
                payloadBytes = Decompress(payloadBytes);

            XmlReader reader = XmlReader.Create(new MemoryStream(payloadBytes));

            System.Data.DataSet dataset = new System.Data.DataSet();
            dataset.ReadXml(reader);

            dataset.WriteXml("Metadata.xml");

            StatusMessage($"Parsed .NET data set with {dataset.Tables.Count:N0} tables from received XML metadata payload.");
        }

        private static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int BufferSize = 4096;
                byte[] buffer = new byte[BufferSize];

                using (MemoryStream memory = new MemoryStream())
                {
                    int count;

                    do
                    {
                        count = stream.Read(buffer, 0, BufferSize);

                        if (count > 0)
                            memory.Write(buffer, 0, count);
                    }
                    while (count > 0);

                    return memory.ToArray();
                }
            }
        }

        protected override void ParsedMetadata()
        {
            StatusMessage("Metadata successfully parsed.");
        }

        public override void SubscriptionUpdated(SignalIndexCache signalIndexCache)
        {
            StatusMessage($"Publisher provided {signalIndexCache.Count} measurements in response to subscription.");
        }

        protected override void ConfigurationChanged()
        {
            StatusMessage("Configuration change detected. Metadata refresh requested.");
        }

        protected override void HistoricalReadComplete()
        {
            StatusMessage("Historical data read complete.");
        }

        protected override void ConnectionEstablished()
        {
            StatusMessage("Connection established.");
        }

        protected override void ConnectionTerminated()
        {
            StatusMessage("Connection terminated.");
        }
    }
}
