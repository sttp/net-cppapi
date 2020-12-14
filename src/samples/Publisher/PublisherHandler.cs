//******************************************************************************************************
//  PublisherHandler.cs - Gbtc
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
//  06/25/2019 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using sttp;
using System;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Publisher
{
    public class PublisherHandler : PublisherInstance
    {
        private readonly string m_name;
        private ulong m_processCount;
        private Timer m_publishTimer;
        private int m_metadataVersion;
        private readonly DeviceMetadataCollection m_deviceMetadata;
        private readonly MeasurementMetadataCollection m_measurementMetadata;
        private readonly PhasorMetadataCollection m_phasorMetadata;

        private static readonly object s_consoleLock = new object();

        public PublisherHandler(string name)
        {
            m_name = name;
            m_deviceMetadata = new DeviceMetadataCollection();
            m_measurementMetadata = new MeasurementMetadataCollection();
            m_phasorMetadata = new PhasorMetadataCollection();
        }

        protected override void StatusMessage(string message)
        {
            // TODO: Make sure these messages get logged to an appropriate location

            // Calls can come from multiple threads, so we impose a simple lock before write to console
            lock (s_consoleLock)
                Console.WriteLine($"[{m_name}] {message}\n");
        }

        protected override void ErrorMessage(string message)
        {
            // TODO: Make sure these messages get logged to an appropriate location

            // Calls can come from multiple threads, so we impose a simple lock before write to console
            lock (s_consoleLock)
                Console.Error.WriteLine($"[{m_name}] {message}\n");
        }

        protected override void ClientConnected(SubscriberConnection connection) => 
            StatusMessage($"Client \"{connection.GetConnectionID()}\" with subscriber ID {connection.GetSubscriberID()} connected...\n\n");

        protected override void ClientDisconnected(SubscriberConnection connection) => 
            StatusMessage($"Client \"{connection.GetConnectionID()}\" with subscriber ID {connection.GetSubscriberID()} disconnected...\n\n");

        // In this example we use predefined structures to setup synchrophasor style metadata. This is only for setup simplification of
        // the initial target uses cases that interact with IEEE C37.118. Technically the publisher can create its own metadata sets.
        private void DefineMetadata()
        {
            // This sample just generates random Guid measurement and device identifiers - for a production system,
            // these Guid values would need to persist between runs defining a permanent association between the
            // defined metadata and the identifier...

            DeviceMetadata device1Metadata = new DeviceMetadata();
            DateTime timestamp = DateTime.UtcNow;

            // Add a device
            device1Metadata.Name = "Test PMU";
            device1Metadata.Acronym = device1Metadata.Name.Replace(" ", "").ToUpper();
            device1Metadata.UniqueID = Guid.NewGuid();
            device1Metadata.Longitude = 300;
            device1Metadata.Latitude = 200;
            device1Metadata.FramesPerSecond = 30;
            device1Metadata.ProtocolName = "STTP";
            device1Metadata.UpdatedOn = timestamp;

            m_deviceMetadata.Add(device1Metadata);

            string pointTagPrefix = device1Metadata.Acronym + ".";
            string measurementSource = "PPA:";
            int runtimeIndex = 1;

            // Add a frequency measurement
            MeasurementMetadata measurement1Metadata = new MeasurementMetadata();
            measurement1Metadata.ID = $"{measurementSource}{runtimeIndex++}";
            measurement1Metadata.PointTag = pointTagPrefix + "FREQ";
            measurement1Metadata.SignalID = Guid.NewGuid();
            measurement1Metadata.DeviceAcronym = device1Metadata.Acronym;
            measurement1Metadata.Reference.Acronym = device1Metadata.Acronym;
            measurement1Metadata.Reference.Kind = SignalKind.Frequency;
            measurement1Metadata.Reference.Index = 0;
            measurement1Metadata.PhasorSourceIndex = 0;
            measurement1Metadata.UpdatedOn = timestamp;

            // Add a dF/dt measurement
            MeasurementMetadata measurement2Metadata = new MeasurementMetadata();
            measurement2Metadata.ID = $"{measurementSource}{runtimeIndex++}";
            measurement2Metadata.PointTag = pointTagPrefix + "DFDT";
            measurement2Metadata.SignalID = Guid.NewGuid();
            measurement2Metadata.DeviceAcronym = device1Metadata.Acronym;
            measurement2Metadata.Reference.Acronym = device1Metadata.Acronym;
            measurement2Metadata.Reference.Kind = SignalKind.DfDt;
            measurement2Metadata.Reference.Index = 0;
            measurement2Metadata.PhasorSourceIndex = 0;
            measurement2Metadata.UpdatedOn = timestamp;

            // Add a phase angle measurement
            MeasurementMetadata measurement3Metadata = new MeasurementMetadata();
            measurement3Metadata.ID = $"{measurementSource}{runtimeIndex++}";
            measurement3Metadata.PointTag = pointTagPrefix + "VPHA";
            measurement3Metadata.SignalID = Guid.NewGuid();
            measurement3Metadata.DeviceAcronym = device1Metadata.Acronym;
            measurement3Metadata.Reference.Acronym = device1Metadata.Acronym;
            measurement3Metadata.Reference.Kind = SignalKind.Angle;
            measurement3Metadata.Reference.Index = 1;   // First phase angle
            measurement3Metadata.PhasorSourceIndex = 1; // Match to Phasor.SourceIndex = 1
            measurement3Metadata.UpdatedOn = timestamp;

            // Add a phase magnitude measurement
            MeasurementMetadata measurement4Metadata = new MeasurementMetadata();
            measurement4Metadata.ID = $"{measurementSource}{runtimeIndex++}";
            measurement4Metadata.PointTag = pointTagPrefix + "VPHM";
            measurement4Metadata.SignalID = Guid.NewGuid();
            measurement4Metadata.DeviceAcronym = device1Metadata.Acronym;
            measurement4Metadata.Reference.Acronym = device1Metadata.Acronym;
            measurement4Metadata.Reference.Kind = SignalKind.Magnitude;
            measurement4Metadata.Reference.Index = 1;   // First phase magnitude
            measurement4Metadata.PhasorSourceIndex = 1; // Match to Phasor.SourceIndex = 1
            measurement4Metadata.UpdatedOn = timestamp;

            m_measurementMetadata.Add(measurement1Metadata);
            m_measurementMetadata.Add(measurement2Metadata);
            m_measurementMetadata.Add(measurement3Metadata);
            m_measurementMetadata.Add(measurement4Metadata);

            // Add a phasor
            PhasorMetadata phasor1Metadata = new PhasorMetadata();
            phasor1Metadata.DeviceAcronym = device1Metadata.Acronym;
            phasor1Metadata.Label = device1Metadata.Name + " Voltage Phasor";
            phasor1Metadata.Type = "V";      // Voltage phasor
            phasor1Metadata.Phase = "+";     // Positive sequence
            phasor1Metadata.SourceIndex = 1; // Phasor number 1
            phasor1Metadata.UpdatedOn = timestamp;

            m_phasorMetadata.Add(phasor1Metadata);

            m_metadataVersion++;

            // Pass meta-data to publisher instance for proper conditioning
            base.DefineMetadata(m_deviceMetadata, m_measurementMetadata, m_phasorMetadata, m_metadataVersion);
        }

        public override bool Start(ushort port)
        {
            if (!base.Start(port))
                return false;

            int maxConnections = MaximumAllowedConnections;
            StatusMessage($"\nListening on port: {GetPort()}, max connections = {(maxConnections == -1 ? "unlimited" : maxConnections.ToString())}...\n");

            // Setup meta-data
            DefineMetadata();

            // Setup data publication timer - for this publishing sample we send
            // data type reasonable random values every 33 milliseconds
            m_publishTimer = new Timer(33) { AutoReset = true };
            m_publishTimer.Elapsed += (sender, e) => PublishRandomValues();

            // Start data publication
            m_publishTimer.Start();

            return true;
        }

        public override void Stop()
        {
            base.Stop();
            m_publishTimer?.Stop();
        }

        private void PublishRandomValues()
        {
            const ulong Interval = 1000;

            // If metadata can change, the following integer should not be static:
            int count = m_measurementMetadata.Count;
            long timestamp = RoundToSubsecondDistribution(DateTime.UtcNow.Ticks, 30);
            Measurement[] measurements = new Measurement[count];
            Random rand = new Random();

            // Create new measurement values for publication
            for (int i = 0; i < count; i++)
            {
                MeasurementMetadata metadata = m_measurementMetadata[i];
                Measurement measurement = new Measurement(metadata.SignalID, timestamp);

                double randFraction = rand.NextDouble();
                double sign = randFraction > 0.5D ? 1.0D : -1.0D;

                measurement.Value = metadata.Reference.Kind switch
                {
                    SignalKind.Frequency => 60.0D + sign * randFraction * 0.1D,
                    SignalKind.DfDt      => sign * randFraction * 2.0D,
                    SignalKind.Magnitude => 500.0D + sign * randFraction * 50.0D,
                    SignalKind.Angle     => sign * randFraction * 180.0D,
                                       _ => sign * randFraction * uint.MaxValue
                };

                measurements[i] = measurement;
            }

            // Publish measurements
            PublishMeasurements(measurements);

            // Display a processing message every few seconds
            bool showMessage = m_processCount + (ulong)count >= (m_processCount / Interval + 1) * Interval && GetTotalMeasurementsSent() > 0;
            m_processCount += (ulong)count;

            if (showMessage)
                StatusMessage($"{GetTotalMeasurementsSent()} measurements published so far...\n");
        }

        // Returns the nearest sub-second distribution timestamp for given ticks.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long RoundToSubsecondDistribution(long ticks, int samplesPerSecond)
        {
            const long TicksPerSecond = TimeSpan.TicksPerSecond;

            // Baseline timestamp to the top of the second
            long baseTicks = ticks - ticks % TicksPerSecond;

            // Remove the seconds from ticks
            long ticksBeyondSecond = ticks - baseTicks;

            // Calculate a frame index between 0 and m_framesPerSecond - 1,
            // corresponding to ticks rounded to the nearest frame
            long frameIndex = (long)Math.Round(ticksBeyondSecond / (TicksPerSecond / (double)samplesPerSecond));

            // Calculate the timestamp of the nearest frame
            long destinationTicks = frameIndex * TicksPerSecond / samplesPerSecond;

            // Recover the seconds that were removed
            destinationTicks += baseTicks;

            return destinationTicks;
        }
    }
}
