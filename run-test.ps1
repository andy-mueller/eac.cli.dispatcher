
& "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" .\cli.dispatcher.sln /t:clean
& "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe" .\cli.dispatcher.sln /t:build
.\packages\xunit.runner.console.2.1.0\tools\xunit.console.exe .\cli.dispatcher.test\bin\Debug\cli.dispatcher.test.dll
