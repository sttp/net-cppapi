if not exist lib md lib
copy ..\build\output\x64\Release\lib\sttp.cs.lib.dll lib
copy lib\sttp.cs.lib.dll sttp.cs
dotnet build -c Release /p:Platform=x64