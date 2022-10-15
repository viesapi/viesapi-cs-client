@echo off

setlocal

set output=%~dp0\viesapiLibrary\bin\Release\com

"%systemroot%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe" /tlb /codebase /nologo "%output%\viesapiLibrary.dll"
"%systemroot%\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" /tlb /codebase /nologo "%output%\viesapiLibrary.dll"

endlocal