pushd %~dp0

del log.txt
SET CORECLR_PROFILER_PATH_64=C:\git\CLRInstrumentationEngine\bin\Debug\x64\MicrosoftInstrumentationEngine_x64.dll
SET CORECLR_PROFILER={324F817A-7420-4E6D-B3C1-143FBED6D855}
SET CORECLR_ENABLE_PROFILING=1
SET MicrosoftInstrumentationEngine_LogLevel=All
SET MicrosoftInstrumentationEngine_FileLogPath=log.txt
SET MicrosoftInstrumentationEngine_ConfigPath64_CLRIEProfiler=C:\git\marcorossignoli.github.io\src\Profilers\artifacts\CLRIEProfiler\x64\Debug\CLRIEProfiler.xml
SET MicrosoftInstrumentationEngine_DisableCodeSignatureValidation=1
SET MicrosoftInstrumentationEngine_DebugWait=1

SampleConsole.exe

popd
