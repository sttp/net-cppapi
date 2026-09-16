# ConnectionTest

.NET console test targeting .NET 10 (preferred), .NET 9, and .NET 6. Uses the local STTP wrapper and matching Windows native library.

From the repository root:

```powershell
dotnet run --project src/test/ConnectionTest -c Release -f net10.0 -- 127.0.0.1 7165
```

Usage: `ConnectionTest <IP> [port]`. IP is required; port defaults to 7165. IPv4 and IPv6 literals are accepted. Use `--help` for usage.

The subscription filter is exactly `FILTER ActiveMeasurements WHERE SignalType <> 'STAT'`.

The app reports elapsed time from the API's connection-established callback to the first nonempty measurement callback, plus total time from initiating the connection attempt. All timings use a monotonic stopwatch; publisher measurement timestamps are not used. Startup timing includes the API's normal metadata/subscription exchange.

A separate metadata-processing duration measures the base metadata callback, including decompression, XML parsing, and construction of native metadata/configuration structures. It excludes waiting for and transferring the metadata payload. Each metadata callback reports its own processing duration; the connection-to-first-measurement timer continues across metadata processing.

Durations show only nonzero hours, minutes, seconds, and milliseconds, for example `125.432 ms`, `2 min 30 s`, or `1 h 4 s 12 ms`. Hours can exceed 24. Fractional milliseconds retain up to three decimal places; a zero duration displays `0 ms`.

After a blank line, the app updates measurements per second on the same line approximately once per second, using the actual elapsed interval. The spinner advances when that interval contains measurements and stays still at zero activity. Press any key to disconnect and exit, including while waiting for connection/data. Ctrl+C also exits. Redirected output uses separate lines; with redirected input use Ctrl+C. Automatic reconnection is disabled so a test run measures one connection.

The default build is Windows x64. The project copies the matching prebuilt `sttp.net.lib.dll` from `build/output` into its output/publish directory. Explicit build:

```powershell
dotnet build src/test/ConnectionTest -c Release -p:Platform=x64
```

Requires the .NET 10 SDK to build all targets and the selected .NET runtime to run. A multi-target build compiles all three frameworks; run/publish commands require `-f net10.0`, `-f net9.0`, or `-f net6.0`. Keep the generated managed and native DLLs alongside the executable when copying the output to another machine.


See the [build instructions](../../lib/README.md) for Boost 1.92.0, native wrapper builds, and publishing applications.
