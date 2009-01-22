@echo off
setlocal

set conf=Release
set v35msbuild=%windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set v20msbuild=%windir%\Microsoft.NET\Framework\v2.0.50727\MSBuild.exe

if exist %v35msbuild% (
	%v35msbuild% /p:Configuration=%conf% /p:Platform=x86 GExplorer.sln
) else if exist %v20msbuild% (
	%v20msbuild% /p:Configuration=%conf% /p:Platform=x86 GExplorer\GExplorer.csproj
) elseÅ@(
	echo MSBuild Ç™å©Ç¬Ç©ÇÁÇ»Ç©Ç¡ÇΩ
)

endlocal
