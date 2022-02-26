pushd %~dp0
SET CORECLR_PROFILER_PATH_64=..\..\..\ProfilerBasics\x64\Debug\ProfilerBasics.dll
SET CORECLR_PROFILER={cf0d821e-299b-5307-a3d8-b283c03916dd}
SET CORECLR_ENABLE_PROFILING=1

SampleConsole.exe

popd
