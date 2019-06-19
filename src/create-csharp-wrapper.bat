@del /Q sttp.cs\*.cs
swig -c++ -csharp -outdir sttp.cs -namespace sttp -dllimport sttp.cs.lib -o sttp.cs.lib/sttp.cs.lib.cpp sttp.i
@pause