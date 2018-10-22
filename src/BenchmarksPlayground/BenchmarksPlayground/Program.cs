using BenchmarkDotNet.Running;
using System;
namespace BenchmarksPlayground
{
    internal class Program
    {
        private static void Main(String[] args)
        {
            // BenchmarkRunner.Run<OutRefMutability>();
            // BenchmarkRunner.Run<ValueTypeRefTypes>();
            BenchmarkRunner.Run<VirtualCall>();
        }
    }
}
