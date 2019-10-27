using System;
using System.Runtime.CompilerServices;

namespace Clr
{
    // if ((uint)index >= (uint)array.Length https://github.com/dotnet/corert/pull/6965#discussion_r255924605
    // https://github.com/dotnet/coreclr/pull/27480 :)
    class MyTest
    {
        [MethodImpl(methodImplOptions: MethodImplOptions.NoInlining)]
        public bool Check(string[] array, int index)
        {
            if (index < 0 || index >= array.Length)
            {
                return false;
            }
            return true;
        }

        [MethodImpl(methodImplOptions: MethodImplOptions.NoInlining)]
        public bool CheckOptimized(string[] array, int index)
        {
            if ((uint)index >= (uint)array.Length)
            {
                return false;
            }
            return true;
        }
    }
}
