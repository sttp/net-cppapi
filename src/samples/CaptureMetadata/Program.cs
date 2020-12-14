//******************************************************************************************************
//  Program.cs - Gbtc
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

namespace CaptureMetadata
{
    class Program
    {
        static void Main(string[] args)
        {
            const string DefaultFilename = "Metadata.xml";

            // Ensure that the necessary
            // command line arguments are given.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("    CaptureMetadata HOSTNAME PORT [FILENAME]");
                return;
            }

            // Get hostname and port.
            string hostname = args[0];
            ushort port = ushort.Parse(args[1]);
            string filename = args.Length > 2 ? args[2] : DefaultFilename;

            // Initialize the subscriber.
            SubscriberHandler subscriber = new SubscriberHandler(filename);
            subscriber.Initialize(hostname, port);
            subscriber.ConnectAsync();

            // Wait until the user presses a key before quitting.
            Console.ReadKey();

            // Shutdown subscriber instance.
            subscriber.Disconnect();

            // Disconnect the subscriber to stop background threads.
            Console.WriteLine("Disconnected.");
        }
    }
}
