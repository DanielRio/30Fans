@echo off

@set PATH=C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE;C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\BIN;%PATH%
::@set INCLUDE=c:\Program Files\Microsoft Visual Studio 9.0\VC\ATLMFC\INCLUDE;c:\Program Files\Microsoft Visual Studio 9.0\VC\INCLUDE;%INCLUDE%
::@set LIB=c:\Program Files\Microsoft Visual Studio 9.0\VC\ATLMFC\LIB;c:\Program Files\Microsoft Visual Studio 9.0\VC\LIB;%LIB%
::@set LIBPATH=c:\WINDOWS\Microsoft.NET\Framework\v3.5;c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727;c:\Program Files\Microsoft Visual Studio 9.0\VC\ATLMFC\LIB;c:\Program Files\Microsoft Visual Studio 9.0\VC\LIB;%LIBPATH%
 
setlocal
set msbuild="C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
set mstest="C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\MSTest.exe"
 
set /p configChoice=Choose your build configuration (Debug = d, Release = r? (d, r)
 
if /i "%configChoice:~,1%" EQU "D" set config=Debug
if /i "%configChoice:~,1%" EQU "R" set config=Release
 
::C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe ci.msbuild.build /nologo /m /v:m /t:Compile /p:Configuration=%config%
%msbuild% ci.msbuild.build /nologo /m /v:m /t:BuildComplete /p:Configuration=%config% /l:XmlLogger,build/Kobush.Build.dll;build/msbuild-output.xml
%msbuild% ci.msbuild.build /t:TransformLog


pause
endlocal