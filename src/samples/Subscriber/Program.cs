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

using sttp;
using System;

namespace Subscriber
{
    class Program
    {
        private const int TotalInstances = 3;
        private static readonly SubscriberHandler[] Subscribers = new SubscriberHandler[TotalInstances];

        static void Main(string[] args)
        {
            // Ensure that the necessary
            // command line arguments are given.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("    Subscriber HOSTNAME PORT");
                return;
            }

            // Get hostname and port.
            string hostname = args[0];
            ushort port = ushort.Parse(args[1]);
            bool usePortOffset = args.Length > 2 && bool.TryParse(args[2], out bool value) && value;

            // Initialize the subscribers.
            for (int i = 0; i < TotalInstances; i++)
            {
                SubscriberHandler subscriber = new SubscriberHandler($"Subscriber {i + 1}");
                subscriber.Initialize(hostname, (ushort)(port + (usePortOffset ? i : 0)));

                switch (i)
                {
                    case 0:
                        subscriber.FilterExpression = "FILTER TOP 10 ActiveMeasurements WHERE SignalType = 'FREQ'";
                        break;
                    case 1:
                        subscriber.FilterExpression = "FILTER TOP 10 ActiveMeasurements WHERE SignalType LIKE '%PHA'";

                        // In this example we also specify a meta-data filtering expression:
                        subscriber.MetadataFilters = SubscriberInstance.FilterMetadataStatsExpression;
                        break;
                    case 2:
                        subscriber.FilterExpression = "FILTER TOP 10 ActiveMeasurements WHERE SignalType LIKE '%PHM'";
                        break;
                    default:
                        subscriber.FilterExpression = SubscriberInstance.SubscribeAllNoStatsExpression;
                        break;
                }

                subscriber.ConnectAsync();
                Subscribers[i] = subscriber;
            }

            // Wait until the user presses enter before quitting.
            Console.ReadLine();

            // Shutdown subscriber instances
            for (int i = 0; i < TotalInstances; i++)
                Subscribers[i].Disconnect();

            // Disconnect the subscriber to stop background threads.
            Console.WriteLine("Disconnected.");
        }
    }
}
