using DictionaryConcurrentAccessDetection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;

namespace ConsoleApp1
{


    public class Program
    {

        static void Main(string[] args)
        {
            Tests.Add_DictionaryConcurrentAccessDetection_NullComparer_ValueTypeKey();
            Tests.Add_DictionaryConcurrentAccessDetection_Comparer_ValueTypeKey();
        }
    }
}
