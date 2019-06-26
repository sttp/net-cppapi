if not exist lib md lib
copy ..\build\output\x86\Release\lib\sttp.cs.lib.dll lib
copy lib\sttp.cs.lib.dll sttp.cs
dotnet build -c Release /p:Platform=x86