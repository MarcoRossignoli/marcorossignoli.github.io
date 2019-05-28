﻿using System;

namespace Clr
{
    // https://github.com/dotnet/coreclr/blob/master/Documentation/building/debugging-instructions.md
    // https://github.com/dotnet/coreclr/blob/master/Documentation/botr/ryujit-overview.md
    // https://github.com/dotnet/coreclr/blob/04fed62162092da2a824e425aa65b8fcfc70ce14/Documentation/botr/ryujit-overview.md#phases-of-ryujit
    class Program
    {
        static void Main(string[] args)
        {
            MyTest mt = new MyTest();
            Console.WriteLine(mt.Check(new string[5], 6));
            Console.WriteLine(mt.CheckOptimized(new string[5], 6));
        }
    }
}
