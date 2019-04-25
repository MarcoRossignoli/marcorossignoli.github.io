using System;
using System.Runtime.CompilerServices;

namespace Clr
{
    class MyTest
    {
        [MethodImpl(methodImplOptions: MethodImplOptions.NoInlining)]
        public bool Check(string[] array, int index)
        {
            // if ((uint)index >= (uint)array.Length https://github.com/dotnet/corert/pull/6965#discussion_r255924605
            if (
                    index < 0 || index >= array.Length
                    // (uint)index >= (uint)array.Length
                )
            {
                return false;
            }
            return true;
        }
    }
}
