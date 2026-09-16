# net-cppapi

<img align="right" src="https://raw.githubusercontent.com/sttp/cppapi/main/src/sttp.png">

.NET APIs for Streaming Telemetry Transport Protocol (STTP), wrapping the [STTP C++ API](https://github.com/sttp/cppapi).

## Use the library

Install the [sttp.net NuGet package](https://www.nuget.org/packages/sttp.net/):

```powershell
dotnet add package sttp.net --version 1.1.3
```

The library supports .NET 6, .NET 9, and .NET 10. The package contains native libraries for Windows x86, Windows x64, and glibc-based Linux x64. Boost is included in the native libraries; applications do not require a separate Boost installation.

## Build from source

Use the .NET 10 SDK. The library, samples, and tests target `net10.0;net9.0;net6.0`, with .NET 10 first. Builds compile all three targets; run and publish commands require an explicit framework, such as `-f net10.0`. Framework-dependent applications require the matching .NET runtime.

Native builds use Boost 1.92.0 (`boost_1_92_0`), C++20, and the platform tools described in the [build instructions](src/lib/README.md). The repository includes native binaries for building the managed samples without compiling C++.

* [Clone and update the C++ submodule](CloneWithSubmodule.md)
* [Build and package instructions](src/lib/README.md)
* [Publisher sample](src/samples/Publisher)
* [Subscriber sample](src/samples/Subscriber)
* [Connection timing test](src/test/ConnectionTest)
* [Change log](src/lib/ChangeLog.md)
