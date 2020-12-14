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

using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using sttp;

namespace CaptureMetadata
{
    public class SubscriberHandler : SubscriberInstance
    {
        private readonly string m_filename;

        public SubscriberHandler(string filename)
        {
            m_filename = filename;
            FilterExpression = Guid.Empty.ToString(); // Subscribe to nothing
        }

        protected override void ReceivedMetadata(ByteBuffer payload)
        {
            byte[] payloadBytes = payload.ToArray();

            StatusMessage($"Received {payloadBytes.Length:N0} bytes of metadata, parsing...");

            if (MetadataCompressed)
                payloadBytes = Decompress(payloadBytes);

            XmlDocument doc = new XmlDocument();
            doc.Load(new MemoryStream(payloadBytes));
            doc.Save(m_filename);

            StatusMessage("Save complete. Press any key to exit.");

            // We don't provide metadata to base class - we are not subscribing to data and do not need native config structures
            //base.ReceivedMetadata(payload);
        }

        protected override void ConnectionEstablished() => StatusMessage("Connection established.");

        protected override void ConnectionTerminated() => StatusMessage("Connection terminated.");

        private static byte[] Decompress(byte[] gzip)
        {
            using GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress, false);
            using MemoryStream memory = new MemoryStream();

            stream.CopyTo(memory);

            return memory.ToArray();
        }
    }
}
