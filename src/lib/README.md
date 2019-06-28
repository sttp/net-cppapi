### STTP .NET Wrapper Library for the CPPAPI

The `sttp-libraries` solution is used to compile C++ code, including the STTP C++ library `sttp.cpp`, the C++ STTP wrapper library for .NET `sttp.net.lib` and the multi-framework target .NET library `sttp.net` which also builds the NuGet package.

Note that compiled libraries resulting from the `sttp.net.lib` project, e.g., `sttp.net.lib.dll`, are pre-compiled and included in the Git repository [output folder](../../build/ouput) so users only wanting to compile the samples can do so without needing to compile the STTP C++ libraries which take considerably more setup and compile time.

#### Recompiling Wrapper Code

To properly compile `cppapi` library, [see build steps](https://github.com/sttp/cppapi/blob/master/src/README.txt).

Note that for compiling on Windows, boost folder needs to be relative to cppapi submodule, i.e., in a path parallel to `src/cppapi`, e.g.:

`mklink /D C:\projects\sttp\net-cppapi\src\boost C:\boost_1_66_0`


#### Rebuilding SWIG Wrappers

Rebuild wrapper code in the `sttp.net.lib` and `sttp.net` projects from the SWIG source file [`sttp.i`](sttp.i) can be accomplished by executing the [create-csharp-wrapper](create-csharp-wrapper.bat) script. This requires [SWIG](http://www.swig.org/) already be available in path. At writing, this code was compiled with SWIG version 4.0.
