@ECHO OFF
ECHO Rebuild STTP .NET Wrapper Libraries
ECHO.
ECHO This script will rebuild all pre-compiled .NET wrapper library targets, specifically:
ECHO -   build/output/x86/Debug/lib/sttp.net.lib.dll
ECHO -   build/output/x86/Release/lib/sttp.net.lib.dll
ECHO -   build/output/x64/Debug/lib/sttp.net.lib.dll
ECHO -   build/output/x64/Release/lib/sttp.net.lib.dll
ECHO.
ECHO Before a production build, make sure to increment version numbers in sttp.net.lib (sttp.net.lib.rc) and sttp.net projects so the updated NuGet nuspec can be deployed.
ECHO.
ECHO Note that "msbuild" will need to be in the path before this script will run.
ECHO.
ECHO Build will start after pause is released:
pause
ECHO.
ECHO Building Debug x86
ECHO.
msbuild /p:Configuration=Debug /p:Platform=x86
ECHO.
ECHO Building Release x86
ECHO.
msbuild /p:Configuration=Release /p:Platform=x86
ECHO.
ECHO Building Debug x64
ECHO.
msbuild /p:Configuration=Debug /p:Platform=x64
ECHO.
ECHO Building Release x64
ECHO.
msbuild /p:Configuration=Release /p:Platform=x64
ECHO.
ECHO Builds complete.
pause