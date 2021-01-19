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
//  01/18/2021 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using Gemstone;

namespace TemporalSubscriber
{
    internal class Program
    {
        // Set temporal parameters for historial read - note that the publisher is not
        // obligated to honor requested parameters. Large historical windows or very
        // fast replay rates may be refused.
        private const string StartTime = "*-5m";    // Start historical replay starting 5-minutes ago (UTC)
        private const string StopTime = "*";        // Stop historical replay at current time (UTC)
        private const int ProcessInterval = 50;     // Set historical replay interval to 50 milliseconds

        private static void Main(string[] args)
        {
            // Ensure that the necessary command line arguments are provided
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("    TemporalSubscriber HOSTNAME PORT");
                return;
            }

            // Get hostname and port
            string hostname = args[0];

            if (!ushort.TryParse(args[1], out ushort port))
                port = 7165;

            long processStartTime = DateTime.UtcNow.Ticks;

            SubscriberHandler subscriber = new SubscriberHandler
            {
                // Set the processing interval, in milliseconds, for the temporal subscription.
                // With the exception of the values of -1 and 0, this value specifies the desired
                // processing interval for data, i.e., basically a delay, or timer interval, over
                // which to process data. A value of -1 means to use the default processing
                // interval while a value of 0 means to process data as fast as possible.
                ProcessInterval = ProcessInterval,

                // Assign handler to run when historical read has completed
                ReadCompleteHandler = (subscriptionCount, totalMeasurementsProcessed) =>
                {
                    Console.Write($"For time range \"{StartTime}\" to \"{StopTime}\":{Environment.NewLine}    ");

                    if (totalMeasurementsProcessed == 0UL)
                    {
                        Console.WriteLine($"No historical data was read for {subscriptionCount:N0} subscribed points");
                    }
                    else
                    {
                        Ticks totalRuntime = DateTime.UtcNow.Ticks - processStartTime;
                        Console.WriteLine($"Processed {totalMeasurementsProcessed:N0} measurements in {totalRuntime.ToElapsedTimeString(1)} ({totalMeasurementsProcessed / totalRuntime.ToSeconds():N0} measurements / second).");
                    }
                }
            };

            subscriber.Initialize(hostname, port);

            // When the StartTime and StopTime temporal processing constraints are defined,
            // the values specify the start and stop time over which the subscriber session
            // desires data. When the parameters are defined, the subscriber is said to be
            // requesting a temporal data session, i.e., requesting to replay history, this
            // assuming the publisher stores history and supports temporal requests.
            //
            // Passing in null for the parameter values (the default) specifies that the
            // subscriber session desires instead to process data in real-time mode, i.e.,
            // subscription of published data as measured.
            //
            // The parameter values are string based so that the times can be set absolute
            // or relative times. The following are example formats for the temporal
            // processing start and stop time parameters:
            //
            //  Example:                  Description:
            //  ------------------------  -------------------------------------------------
            //  12-30-2000 23:59:59.033   specifies an absolute date and time
            //            *               evaluates to current time (UTC)
            //          *-20s             evaluates to 20 seconds before current time (UTC)
            //          *-10m             evaluates to 10 minutes before current time (UTC)
            //          *-1h              evaluates to 1 hour before current time (UTC)
            //          *-1d              evaluates to 1 day before current time (UTC)
            subscriber.EstablishHistoricalRead(StartTime, StopTime);

            subscriber.FilterExpression = "FILTER TOP 10 ActiveMeasurements WHERE SignalType = 'FREQ' OR SignalType LIKE '%PH%'";

            subscriber.ConnectAsync();

            // Wait until the user presses a key before quitting.
            Console.ReadKey();

            // Shutdown subscriber instances
            subscriber.Disconnect();

            Console.WriteLine("Disconnected.");
        }
    }
}
