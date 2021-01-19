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
//  01/18/2021 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Text;
using System.Threading;
using sttp;

namespace TemporalSubscriber
{
    public class SubscriberHandler : SubscriberInstance
    {
        private uint m_subscriptionCount;
        private long m_lastMessageTime;
        private static readonly object s_consoleLock = new object();

        // Processing interval, in milliseconds, for the temporal subscription
        public int ProcessInterval { get; init; } = -1;

        // Call back for read complete - parameters are subscribed measurement count and total measurements processed
        public Action<uint, ulong> ReadCompleteHandler { get; init; }

        //protected override SubscriptionInfo CreateSubscriptionInfo()
        //{
        //    SubscriptionInfo info = base.CreateSubscriptionInfo();
        //
        //    // See SubscriptionInfo class in DataSubscriber.h for all properties:
        //    // https://github.com/sttp/cppapi/blob/master/src/lib/transport/DataSubscriber.h#L41
        //
        //    // Examples:
        //    //info.Throttled = false;
        //    //info.IncludeTime = true;
        //    //info.UseMillisecondResolution = true;
        //
        //    return info;
        //}

        protected override void SetupSubscriberConnector(SubscriberConnector connector)
        {
            base.SetupSubscriberConnector(connector);

            // Enable auto-reconnect sequence:
            connector.AutoReconnect = true;

            // Set maximum number to attempt reconnection, -1 means never stop retrying connection attempts:
            connector.MaxRetries = -1;

            // Set number of initial milliseconds to wait before retrying connection attempt:
            connector.RetryInterval = 1000;

            // Set maximum number of milliseconds to wait before retrying connection attempt, connection
            // retry attempts use exponential back-off algorithm up to this defined maximum:
            connector.MaxRetryInterval = 6000;
        }

        protected override void StatusMessage(string message)
        {
            // TODO: Make sure these messages get logged to an appropriate location

            // Calls can come from multiple threads, so we impose a simple lock before write to console
            lock (s_consoleLock)
                Console.WriteLine($"{message}\n");
        }

        protected override void ErrorMessage(string message)
        {
            // TODO: Make sure these messages get logged to an appropriate location

            // Calls can come from multiple threads, so we impose a simple lock before write to console
            lock (s_consoleLock)
                Console.Error.WriteLine($"{message}\n");
        }

        // This reports timestamp of very first received measurement (if useful)
        protected override void DataStartTime(DateTime startTime) => 
            StatusMessage($"Received first measurement at timestamp {startTime:yyyy-MM-dd HH:mm:ss.fff}");

        protected override void ReceivedMetadata(ByteBuffer payload)
        {
            StatusMessage($"Received {payload.Count:N0} bytes of metadata, parsing...");
            base.ReceivedMetadata(payload);
        }

        protected override void ParsedMetadata() => 
            StatusMessage("Metadata successfully parsed.");

        public override void SubscriptionUpdated(SignalIndexCache signalIndexCache)
        {
            m_subscriptionCount = signalIndexCache.Count;
            StatusMessage($"Publisher provided {m_subscriptionCount:N0} measurements in response to subscription.");

            // Establish initial temporal processing interval as soon as subscription is ready. This value
            // can be changed at anytime while historical replay is active.
            SetHistoricalReplayInterval(ProcessInterval);
        }

        public override unsafe void ReceivedNewMeasurements(Measurement* measurements, int length)
        {
            long currentTime = DateTime.UtcNow.Ticks;
            bool showMessage = new TimeSpan(currentTime - m_lastMessageTime).TotalSeconds > 5.0D;

            if (showMessage)
            {
                m_lastMessageTime = currentTime;

                StringBuilder message = new StringBuilder();

                message.AppendLine($"{GetTotalMeasurementsReceived():N0} measurements received so far...");
                message.AppendLine();

                for (int i = 0; i < Math.Min(length, 10); i++)
                {
                    Measurement measurement = measurements[i];
                    message.AppendLine($"({i + 1:D2}) [{measurement.GetSignalID()}] @ {measurement.GetDateTime():yyyy-MM-dd HH:mm:ss.fff}:");
                    message.AppendLine($"\t{measurement.Value:N3}");
                }

                StatusMessage(message.ToString());
            }
        }

        protected override void ConfigurationChanged() => 
            StatusMessage("Configuration change detected. Metadata refresh requested.");

        protected override void HistoricalReadComplete()
        {
            StatusMessage("Historical data read complete.");

            if (ReadCompleteHandler is null)
                return;

            ulong totalMeasurementsProcessed = GetTotalMeasurementsReceived();

            // Call read complete handler on separate thread so as to not
            // hold up base class call for HistoricalReadComplete method
            ThreadPool.QueueUserWorkItem(_ => ReadCompleteHandler(m_subscriptionCount, totalMeasurementsProcessed));
        }

        protected override void ConnectionEstablished() => 
            StatusMessage("Connection established.");

        protected override void ConnectionTerminated() => 
            StatusMessage("Connection terminated.");
    }
}
