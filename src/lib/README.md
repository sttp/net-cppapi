### STTP .NET Wrapper Library for the CPPAPI

The `sttp-libraries` solution is used to compile C++ code, including the STTP C++ library `sttp.cpp`, the C++ STTP wrapper library for .NET `sttp.net.lib` and the multi-framework target .NET library `sttp.net` which also builds the NuGet package.

Note that compiled libraries resulting from the `sttp.net.lib` project, e.g., `sttp.net.lib.dll`, are pre-compiled and included in the Git repository [output folder](../../build/output/x64/Release/lib) so users only wanting to compile the .NET samples can do so without needing to compile the STTP C++ libraries which take considerably more setup and compile time.

#### Recompiling Wrapper Code

To properly compile the `cppapi` library, [see build steps](https://github.com/sttp/cppapi/blob/master/src).

Note that there is one difference for compiling the `sttp.cpp` SWIG target on Windows, the boost folder needs to be relative to `cppapi` submodule, i.e., in the `src/lib/` folder, e.g.:

`mklink /D C:\projects\sttp\net-cppapi\src\lib\boost C:\boost_1_75_0`

When compiling from the command prompt with [`build-libraries.bat`](build-libraries.bat), the boost folder is relative to `cppapi` solution, not the `sttp-libraries` solution, e.g.:

`mklink /D C:\projects\sttp\net-cppapi\src\lib\cppapi\src\boost C:\boost_1_75_0`


#### Rebuilding SWIG Wrappers

Rebuilding wrapper code in the `sttp.net.lib` and `sttp.net` projects from the SWIG source file [`sttp.i`](sttp.i) can be accomplished by executing the [create-csharp-wrapper](create-csharp-wrapper.bat) script. This requires [SWIG](http://www.swig.org/) already be available in path. At writing, this code was compiled with SWIG version 4.0.

#### Deploying Native Dependencies with a Visual Studio Publish Profile

In order to get a Visual Studio folder-based publish profile to properly compile, it may be necessary to modify the STTP project reference in the `.csproj` file as follows:
```xml
    <PackageReference Include="sttp.net" Version="1.0.9">
      <ExcludeAssets>native</ExcludeAssets>
    </PackageReference>
```

Here is an example publish profile that creates a single executable that references `sttp.net.dll` and its native library:
```xml
<?xml version="1.0" encoding="utf-8"?>
<!--
https://go.microsoft.com/fwlink/?LinkID=208121. 
-->
<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <Configuration>Release</Configuration>
    <Platform>Any CPU</Platform>
    <PublishDir>bin\Release\net5.0\publish\</PublishDir>
    <PublishProtocol>FileSystem</PublishProtocol>
    <TargetFramework>net5.0</TargetFramework>
    <SelfContained>true</SelfContained>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <PublishSingleFile>True</PublishSingleFile>
    <PublishReadyToRun>True</PublishReadyToRun>
    <PublishReadyToRunShowWarnings>True</PublishReadyToRunShowWarnings>
    <PublishTrimmed>True</PublishTrimmed>
    <IncludeAllContentForSelfExtract>True</IncludeAllContentForSelfExtract>
    <IncludeNativeLibrariesInSingleFile>True</IncludeNativeLibrariesInSingleFile>
  </PropertyGroup>
</Project>
```
