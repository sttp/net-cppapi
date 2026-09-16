@echo off
setlocal
where swig >nul 2>&1
if errorlevel 1 (
    echo SWIG was not found on PATH. Install SWIG and open a new terminal.
    exit /b 1
)
pushd "%~dp0"
swig -version
rem SWIG overwrites its outputs; do not delete existing bindings before generation.
swig -c++ -csharp -outdir sttp.net -namespace sttp -dllimport sttp.net.lib -o sttp.net.lib/sttp.net.lib.cpp sttp.i
set "RESULT=%ERRORLEVEL%"
popd
exit /b %RESULT%
