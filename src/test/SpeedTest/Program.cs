﻿using sttp;
using System;
using System.Diagnostics;

#pragma warning disable CS0649

namespace SpeedTest
{
    class Measurement
    {
        public ulong ID;
        public Guid SignalID;
        public double Value;
        public double Adder;
        public double Multiplier;
        public long Timestamp;
        public MeasurementStateFlags Flags;

        public double AdjustedValue() => Value * Multiplier + Adder;
        public DateTime GetDateTime() => new DateTime(Timestamp);
    }

    class Program
    {
        private const int Repeats = 10;
        private const int TestTotal = 100000;

        private static void Main()
        {
            double totalProcessingTime = 0.0D;

            for (int i = 0; i < Repeats; i++)
            {
                DateTime startTime = DateTime.UtcNow;

                for (int j = 0; j < TestTotal; j++)
                {
                    Measurement measurement = new Measurement();

                    measurement.SignalID = Guid.NewGuid();
                    measurement.ID = (ulong)j;
                    measurement.Timestamp = startTime.Ticks;
                    measurement.Value = (1 + 1) * (j + 1);

                    DateTime retrieved = measurement.GetDateTime();
                    Debug.Assert((int)(retrieved - startTime).TotalMilliseconds == 0);
                }

                double processingTime = (DateTime.UtcNow - startTime).TotalSeconds;
                Console.WriteLine($"Native run {i + 1} processing time = {processingTime:N4} seconds.");

                totalProcessingTime += processingTime;
            }

            double nativeAverage = totalProcessingTime / Repeats;
            Console.WriteLine();
            Console.WriteLine($"Native average processing time = {nativeAverage:N4} seconds.");
            Console.WriteLine();

            totalProcessingTime = 0.0D;

            for (int i = 0; i < Repeats; i++)
            {
                DateTime startTime = DateTime.UtcNow;

                for (int j = 0; j < TestTotal; j++)
                {
                    SimpleMeasurement measurement = new SimpleMeasurement();

                    measurement.SignalID = Guid.NewGuid();
                    measurement.Timestamp = startTime.Ticks;
                    measurement.Value = (1 + 1) * (j + 1);

                    DateTime retrieved = new DateTime(measurement.Timestamp);
                    Debug.Assert((int)(retrieved - startTime).TotalMilliseconds == 0);
                }

                double processingTime = (DateTime.UtcNow - startTime).TotalSeconds;
                Console.WriteLine($"Wrapped run {i + 1} processing time = {processingTime:N4} seconds.");

                totalProcessingTime += processingTime;
            }

            double wrappedAverage = totalProcessingTime / Repeats;
            Console.WriteLine();
            Console.WriteLine($"Wrapped average processing time = {wrappedAverage:N4} seconds.");
            Console.WriteLine();

            totalProcessingTime = 0.0D;

            for (int i = 0; i < Repeats; i++)
            {
                DateTime startTime = DateTime.UtcNow;

                for (int j = 0; j < TestTotal; j++)
                {
                    sttp.Measurement measurement = new sttp.Measurement();

                    measurement.SetSignalID(Guid.NewGuid());
                    measurement.Timestamp = startTime.Ticks;
                    measurement.Value = (1 + 1) * (j + 1);

                    DateTime retrieved = measurement.GetDateTime();
                    Debug.Assert((int)(retrieved - startTime).TotalMilliseconds == 0);
                }

                double processingTime = (DateTime.UtcNow - startTime).TotalSeconds;
                Console.WriteLine($"Custom marshaled run {i + 1} processing time = {processingTime:N4} seconds.");

                totalProcessingTime += processingTime;
            }

            double customMarshaledAverage = totalProcessingTime / Repeats;
            Console.WriteLine();
            Console.WriteLine($"Custom marshaled average processing time = {customMarshaledAverage:N4} seconds.");
            Console.WriteLine();

            Console.WriteLine($"Difference between SWIG wrapped and native: {wrappedAverage - nativeAverage:N6} seconds");
            Console.WriteLine($"Difference between custom marshaled and native: {customMarshaledAverage - nativeAverage:N6} seconds");

            Console.ReadKey();
        }
    }
}
