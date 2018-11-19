using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Toolchains.CoreRun;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Test
{

    public class MyConfig : ManualConfig
    {
        public MyConfig()
        {
            Job baseJob = Job.Default
                // .WithWarmupCount(10)
                // .WithIterationCount(300)
                // .WithInvocationCount(2097152)
                // .WithOutlierMode(OutlierMode.None)
                ;
            Add(baseJob.With(new CoreRunToolchain(new FileInfo(
                    @"..\..\..\..\corefx\artifacts\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe")))
                .WithId("CoreFx")
            );
            Add(baseJob.With(new CoreRunToolchain(new FileInfo(
                    @"..\..\..\..\corefxupstream\artifacts\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe")))
                .WithId("CoreFxUpstream")
                .AsBaseline()
            );
        }
    }

    [Config(typeof(MyConfig))]
    public class RunImpersonatedBenchmark
    {
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out SafeAccessTokenHandle phToken);

        private const int LOGON32_PROVIDER_DEFAULT = 0;
        private SafeAccessTokenHandle safeAccessTokenHandle;
        //This parameter causes LogonUser to create a primary token.
        private const int LOGON32_LOGON_INTERACTIVE = 2;

        [Params(1000)]
        public int cycles;

        [GlobalSetup]
        public void Setup()
        {
            bool returnValue = LogonUser("OtherUser", ".", File.ReadAllText(@"..\..\..\..\..\pwd.txt"),
                LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
                out this.safeAccessTokenHandle);
        }

        [Benchmark]
        public void RunImpersonated()
        {
            for (int i = 0; i < cycles; i++)
            {
                WindowsIdentity.RunImpersonated(this.safeAccessTokenHandle, () => { });
            }
        }
    }
}
