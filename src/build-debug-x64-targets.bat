if not exist lib md lib
copy ..\build\output\x64\Debug\lib\sttp.cs.lib.dll lib
copy lib\sttp.cs.lib.dll sttp.cs
dotnet build -c Debug /p:Platform=x64