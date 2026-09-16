# Build the STTP .NET wrapper

The `sttp-libraries.sln` solution builds the native STTP library (`sttp.cpp`), its .NET wrapper (`sttp.net.lib`), and the managed library (`sttp.net`).

The managed library, samples, and tests target .NET 6, .NET 9, and .NET 10. Use the .NET 10 SDK to build all targets. .NET 10 is the first target in each project. Run and publish commands require `-f net10.0`, `-f net9.0`, or `-f net6.0`. Framework-dependent applications require the selected .NET runtime.

All commands below start from the repository root unless stated otherwise. Follow the [clone instructions](../../CloneWithSubmodule.md) to initialize the C++ submodule.

## Build managed samples

The repository includes native libraries in [build/output](../../build/output). Building the managed samples does not require Boost, SWIG, or a C++ compiler.

```powershell
dotnet build src/sttp.net.sln -c Release -p:Platform=x64 -p:GeneratePackageOnBuild=false
dotnet run --project src/test/ConnectionTest -c Release -f net10.0 -p:Platform=x64 -- 127.0.0.1 7165
```

Replace the address and port with your STTP publisher's endpoint. See [ConnectionTest](../test/ConnectionTest/README.md) for timing and measurement-rate reporting.

## Build native libraries on Windows

Install Visual Studio 2026 with the C++ v145 tools and the .NET 10 SDK. Build Boost 1.92.0 with the matching toolset, x86/x64 architectures, and zlib support. See the [C++ build instructions](cppapi/src/README.md) for native dependency setup.

Both projects in the wrapper solution expect Boost at `src/lib/boost`. From a command prompt, create a directory link to your Boost installation, adjusting the paths as needed:

```cmd
mklink /D C:\Projects\sttp\net-cppapi\src\lib\boost C:\boost_1_92_0
```

From a Visual Studio developer terminal, build the Release libraries:

```powershell
msbuild src/lib/sttp-libraries.sln /restore /p:Configuration=Release /p:Platform=x64 /p:PreferredToolArchitecture=x64 /p:GeneratePackageOnBuild=false
msbuild src/lib/sttp-libraries.sln /restore /p:Configuration=Release /p:Platform=x86 /p:WholeProgramOptimization=false /p:PreferredToolArchitecture=x64 /p:GeneratePackageOnBuild=false
```

The x86 Release command disables whole-program optimization to avoid the MSVC v145 LNK1000 linker failure. Normal Release optimization remains enabled.

To build both Debug and Release for x86 and x64, run:

```powershell
.\src\lib\build-libraries.bat
```

Both methods use `sttp-libraries.sln` and the same `src/lib/boost` link. Native outputs go to `build/output/<architecture>/<configuration>/lib`. Managed assemblies appear in the `net6.0`, `net9.0`, and `net10.0` subdirectories.

## Build native libraries on Linux or WSL

Install a C++20 compiler, CMake 3.13 or later, Boost 1.92.0 sources, and the zlib/bzip2 development packages. Use Linux Boost libraries when building in WSL.

CMake embeds static Boost libraries in `sttp.net.lib.so`. Build Boost with position-independent code (`-fPIC`). From the Boost source directory:

```bash
./bootstrap.sh
./b2 --build-dir="$HOME/boost-pic-build" --prefix="$HOME/boost-pic" \
    --with-thread --with-date_time --with-iostreams --with-chrono \
    --with-atomic --with-container --with-random \
    toolset=gcc variant=release link=static runtime-link=shared \
    cxxflags=-fPIC -j6 install
```

From the net-cppapi repository root:

```bash
STTP_BUILD_ROOT="$HOME/sttp-net-static-build" \
    bash src/lib/sttp.net.lib/buildso.sh -DBoost_ROOT="$HOME/boost-pic"
```

The script builds Debug and Release from the checked-in SWIG-generated sources. SWIG is only required when regenerating bindings. Set `STTP_BUILD_JOBS` to change the default six parallel jobs. Additional arguments pass through to CMake.

`STTP_BUILD_ROOT` selects the intermediate build directory; the default is `src/lib/sttp.net.lib/bin/linux`. In WSL, a directory on the Linux filesystem avoids intermediate build traffic on the Windows mount. Use a fresh build directory when switching Boost installations.

The x64 libraries copy to `build/output/x64/{Debug,Release}/lib/sttp.net.lib.so`. To build one configuration directly:

```bash
cmake -S src/lib -B "$HOME/sttp-net-release" \
    -DCMAKE_BUILD_TYPE=Release -DBoost_ROOT="$HOME/boost-pic"
cmake --build "$HOME/sttp-net-release" --target sttp.net.lib --parallel 6
```

Direct Linux x64 CMake builds also copy the library to the package input directory. Single-configuration builds default to Release when `CMAKE_BUILD_TYPE` is omitted.

Check runtime dependencies with:

```bash
ldd -r build/output/x64/Release/lib/sttp.net.lib.so
```

The output should contain no unresolved symbols or Boost shared-library dependencies. Linux system libraries, including glibc, libstdc++, and zlib, remain dynamically linked. Build on the oldest distribution you support to keep compatible system-library requirements. The `linux-x64` package asset supports glibc-based x64 Linux; musl and ARM require separate native builds.

## Regenerate SWIG bindings

The repository includes generated bindings. Regenerate them when the native API or [sttp.i](sttp.i) changes, using SWIG 4.5.1 on `PATH`:

```powershell
swig -version
.\src\lib\create-csharp-wrapper.bat
```

The script works from any directory and checks for SWIG before replacing generated files. Rebuild the native and managed wrappers together after regeneration so their interfaces match.

## Create a NuGet package

Build Windows x86/x64 and Linux x64 Release libraries before packing. Keep `GeneratePackageOnBuild=false` during individual builds so packaging uses the complete set of native outputs.

```powershell
dotnet pack src/lib/sttp.net/sttp.net.csproj -c Release -p:Platform=x64 --no-build -o build/packages
```

The package contains:

| Asset | Package path |
| --- | --- |
| .NET 6 assembly | `lib/net6.0/sttp.net.dll` |
| .NET 9 assembly | `lib/net9.0/sttp.net.dll` |
| .NET 10 assembly | `lib/net10.0/sttp.net.dll` |
| Windows x86 native library | `runtimes/win-x86/native/sttp.net.lib.dll` |
| Windows x64 native library | `runtimes/win-x64/native/sttp.net.lib.dll` |
| Linux x64 native library | `runtimes/linux-x64/native/sttp.net.lib.so` |

SDK projects select native assets through NuGet. Keep native assets enabled in `PackageReference`.

## Publish an application

Reference the package from your application:

```xml
<PackageReference Include="sttp.net" Version="1.1.3" />
```

From the application directory, publish for the required runtime:

```powershell
dotnet publish -c Release -f net10.0 -r win-x64 --self-contained false
```

Use `win-x86` or `linux-x64` for the other supported platforms. Select `net9.0` or `net6.0` when the application targets that framework. Deploy the full publish directory, including the native library.

For a self-contained single-file Windows application:

```powershell
dotnet publish -c Release -f net10.0 -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=false
```

The executable includes the .NET runtime and extracts the native library when it runs. For Visual Studio publishing, use the corresponding profile properties:

```xml
<PropertyGroup>
  <Configuration>Release</Configuration>
  <TargetFramework>net10.0</TargetFramework>
  <RuntimeIdentifier>win-x64</RuntimeIdentifier>
  <SelfContained>true</SelfContained>
  <PublishSingleFile>true</PublishSingleFile>
  <IncludeNativeLibrariesForSelfExtract>true</IncludeNativeLibrariesForSelfExtract>
  <PublishTrimmed>false</PublishTrimmed>
</PropertyGroup>
```
