using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using sttp;

namespace ConnectionTest;

internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Length == 1 && args[0] is "--help" or "-h")
        {
            Usage();
            return 0;
        }

        ushort port = 7165;
        if (args.Length is < 1 or > 2 || !IPAddress.TryParse(args[0], out IPAddress? address) ||
            (args.Length == 2 && (!ushort.TryParse(args[1], out port) || port == 0)))
        {
            Usage();
            return 1;
        }

        using var stop = new CancellationTokenSource();
        ConsoleCancelEventHandler cancel = (_, e) => { e.Cancel = true; stop.Cancel(); };
        Console.CancelKeyPress += cancel;
        try
        {
            using var subscriber = new TimingSubscriber();
            subscriber.Initialize(address.ToString(), port);
            subscriber.FilterExpression = "FILTER ActiveMeasurements WHERE SignalType <> 'STAT'";
            subscriber.AutoReconnect = false;
            Console.WriteLine($"Connecting to {address}:{port}");
            Console.WriteLine($"Filter: {subscriber.FilterExpression}");
            Console.WriteLine(Console.IsInputRedirected ? "Press Ctrl+C to exit." : "Press any key to exit.");
            subscriber.Start();
            int width = 0;
            int spinner = 0;
            long previousCount = 0;
            long previousTick = Stopwatch.GetTimestamp();
            bool reported = false;
            try
            {
                while (!stop.IsCancellationRequested)
                {
                    if (!Console.IsInputRedirected && Console.KeyAvailable)
                    {
                        Console.ReadKey(intercept: true);
                        break;
                    }

                    while (subscriber.Messages.TryDequeue(out string? message))
                    {
                        if (width > 0) Console.Write("\r" + new string(' ', width) + "\r");
                        Console.WriteLine(message);
                        width = 0;
                    }

                    long first = subscriber.FirstDataTick;
                    if (!reported && first != 0)
                    {
                        Console.WriteLine($"Connection to first measurement: {FormatDuration(ElapsedTime(subscriber.ConnectedTick, first))}");
                        Console.WriteLine($"Total from connection attempt: {FormatDuration(ElapsedTime(subscriber.StartTick, first))}");
                        Console.WriteLine();
                        reported = true;
                        previousTick = first;
                    }

                    long now = Stopwatch.GetTimestamp();
                    double seconds = ElapsedTime(previousTick, now).TotalSeconds;
                    if (reported && seconds >= 1.0)
                    {
                        long count = subscriber.Count;
                        long received = count - previousCount;
                        if (received > 0) spinner = (spinner + 1) % 4;
                        string line = $"{"\\|/-"[spinner]} {received / seconds,14:N0} measurements/sec";
                        if (Console.IsOutputRedirected) Console.WriteLine(line);
                        else
                        {
                            Console.Write("\r" + line.PadRight(width));
                            width = line.Length;
                        }
                        previousCount = count;
                        previousTick = now;
                    }
                    stop.Token.WaitHandle.WaitOne(50);
                }
            }
            finally
            {
                subscriber.Disconnect();
                if (width > 0) Console.WriteLine();
            }
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"STTP test failed: {ex.GetBaseException().Message}");
            return 1;
        }
        finally
        {
            Console.CancelKeyPress -= cancel;
        }
    }

    // Stopwatch.GetElapsedTime is unavailable on .NET 6.
    private static TimeSpan ElapsedTime(long start, long end) =>
        TimeSpan.FromSeconds((end - start) / (double)Stopwatch.Frequency);

    internal static string FormatDuration(TimeSpan duration)
    {
        // Round to microseconds, preserving fractional milliseconds for short operations.
        long microseconds = (duration.Ticks + 5) / 10;
        long hours = microseconds / 3_600_000_000;
        long minutes = microseconds / 60_000_000 % 60;
        long seconds = microseconds / 1_000_000 % 60;
        double milliseconds = microseconds % 1_000_000 / 1000.0;
        var parts = new List<string>(4);
        if (hours > 0) parts.Add($"{hours} h");
        if (minutes > 0) parts.Add($"{minutes} min");
        if (seconds > 0) parts.Add($"{seconds} s");
        if (milliseconds > 0 || parts.Count == 0) parts.Add($"{milliseconds:0.###} ms");
        return string.Join(" ", parts);
    }
    private static void Usage() => Console.WriteLine("Usage: ConnectionTest <IP> [port]\nPort defaults to 7165 (valid range: 1-65535).");
}

internal sealed class TimingSubscriber : SubscriberInstance
{
    private long m_count;
    private long m_firstDataTick;
    private long m_connectedTick;
    public ConcurrentQueue<string> Messages { get; } = new();
    public long StartTick { get; private set; }
    public long ConnectedTick => Interlocked.Read(ref m_connectedTick);
    public long FirstDataTick => Interlocked.Read(ref m_firstDataTick);
    public long Count => Interlocked.Read(ref m_count);

    public void Start()
    {
        StartTick = Stopwatch.GetTimestamp();
        ConnectAsync();
    }

    protected override void ConnectionEstablished()
    {
        Interlocked.Exchange(ref m_connectedTick, Stopwatch.GetTimestamp());
        Messages.Enqueue("Connected; waiting for measurements...");
    }

    protected override void ReceivedMetadata(ByteBuffer payload)
    {
        Messages.Enqueue("Metadata received; processing...");
        var timer = Stopwatch.StartNew();
        base.ReceivedMetadata(payload);
        timer.Stop();
        Messages.Enqueue($"Metadata processing: {Program.FormatDuration(timer.Elapsed)}");
    }
    public override unsafe void ReceivedNewMeasurements(Measurement* measurements, int length)
    {
        if (length <= 0) return;
        long now = Stopwatch.GetTimestamp();
        Interlocked.Add(ref m_count, length);
        Interlocked.CompareExchange(ref m_firstDataTick, now, 0);
    }

    protected override void StatusMessage(string message) { }
    protected override void ErrorMessage(string message) => Messages.Enqueue($"Error: {message}");
    protected override void ConnectionTerminated() => Messages.Enqueue("Connection terminated. Press any key to exit.");
}
