cls
echo off
SET DIR=%~dp0%
IF NOT EXIST "%DIR%log" MKDIR "%DIR%log"
"%PROGRAMFILES(X86)%\MSBuild\14.0\Bin\MsBuild.exe" /m /v:n "%DIR%DavidLievrouw.Utils.proj" /target:BuildDeploy /logger:FileLogger,Microsoft.Build.Engine;LogFile=%DIR%log/builddeploy.log
pause