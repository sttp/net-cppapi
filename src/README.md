Recompiling wrapper code:

1) Define boost folder in path parallel to `src/cppapi`, e.g.: `mklink /D C:\projects\sttp\net-cppapi\src\boost C:\boost_1_66_0`
2) Compile spp.ccp library: [see build steps](https://github.com/sttp/cppapi/blob/master/src/README.txt)
3) Rebuild code in spp.cs and spp.cs.lib: [execute script](create-csharp-wrapper.bat) - requires [SWIG](http://www.swig.org/) in path