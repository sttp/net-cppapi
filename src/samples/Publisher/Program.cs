using System;

namespace Publisher
{
    class Program
    {
        private const int TotalInstances = 3;
        private static readonly PublisherHandler[] Publishers = new PublisherHandler[TotalInstances];

        static void Main(string[] args)
        {
            ushort port;

            // Ensure that the necessary
            // command line arguments are given.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("    Publisher PORT");
                return;
            }

            // Get port.
            port = ushort.Parse(args[0]);

            // Initialize the publishers.
            for (int i = 0; i < TotalInstances; i++)
            {
                PublisherHandler publisher = new PublisherHandler($"Publisher {i + 1}");

                // Set second publisher to only allow one connection
                if (i == 1)
                    publisher.MaximumAllowedConnections = 1;

                publisher.Start((ushort)(port + i));
                Publishers[i] = publisher;
            }

            // Wait until the user presses enter before quitting.
            Console.ReadLine();

            // Stop publisher instances - this stops publication
            for (int i = 0; i < TotalInstances; i++)
                Publishers[i].Stop();

            // Disconnect the subscriber to stop background threads.
            Console.WriteLine("Publishers stopped.");
        }
    }
}
