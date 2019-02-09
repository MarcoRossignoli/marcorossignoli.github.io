using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Collections.Generic2.Dictionary<string, string> test = new System.Collections.Generic2.Dictionary<string, string>();

            Debug.Assert(test.Count == 0);
            Debug.Assert(test.ToArray().Length == 0);

            foreach (KeyValuePair<string, string> item in test)
            {
                Debug.Assert(false);
            }

            try
            {
                var key = test["Key"];
                Debug.Assert(false);
            }
            catch (KeyNotFoundException)
            {

            }

            List<string> keys = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                // Console.WriteLine(i);
                var key = Guid.NewGuid().ToString("D");
                keys.Add(key);
                test.Add(key, key);
                Debug.Assert(test[key] == key);
            }

            foreach (KeyValuePair<string, string> item in test.ToArray())
            {
                test.Remove(item.Key);
            }
        }
    }
}
