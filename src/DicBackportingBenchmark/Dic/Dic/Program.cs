using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Collections.Generic2.Dictionary<string, string> test = new System.Collections.Generic2.Dictionary<string, string>();            

            bool exc = false;
            try
            {
                var key = test["Key"];
            }
            catch(KeyNotFoundException)
            {
                exc = true;
            }

            List<string> keys = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                // Console.WriteLine(i);
                var key = Guid.NewGuid().ToString("D");
                keys.Add(key);
                test.Add(key, key);
                System.Diagnostics.Debug.Assert(test[key] == key);
            }

            //foreach (var item in test.ToArray())
            //{
            //    test.Remove(item.Key);
            //}
        }
    }
}
