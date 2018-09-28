using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace BenchmarksPlayground
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class ValueTypeRefTypes
    {
        public static void Run()
        {
            Summary summary = BenchmarkRunner.Run<ValueTypeRefTypes>();
        }

        [Benchmark]
        public void AllocateValueType()
        {
            for (int i = 0; i < 1000; i++)
            {
                new ValueType()
                {
                    val = i, // str = i.ToString() 
                };
            }
        }

        [Benchmark]
        public void AllocateRefType()
        {
            for (int i = 0; i < 1000; i++)
            {
                new RefType()
                {
                    val = i, // str = i.ToString() };
                };
            }
        }

        public struct ValueType
        {
            public int val;
            // public string str;
        }

        public class RefType
        {
            public int val;
            // public string str;
        }
    }
}
