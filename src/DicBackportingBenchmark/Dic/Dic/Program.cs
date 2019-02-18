using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Test();
            Test2();
            Test3();
            Test4();
            Test5();
            Test6();
        }

        public static void Test6()
        {
            System.Collections.Generic2.Dictionary<string, string> test = new System.Collections.Generic2.Dictionary<string, string>();
            //test.Add("A", "A");
            //test.Add("B", "B");
            //test.Add("C", "C");

            test.Add("D", "D");
            Debug.Assert(test.Remove("D", out string outValue1));
            Debug.Assert("D" == outValue1);
            Debug.Assert(!test.TryGetValue("D", out string outValue2));
            Debug.Assert(test.Count == 0);

            test.Add("D", "D");

            Debug.Assert(test.Count == 1);

            Debug.Assert(test.Remove("D", out string outValue3));

            Debug.Assert(test.Count == 0);
        }

        public static void Test5()
        {
            System.Collections.Generic2.Dictionary<string, string> test = new System.Collections.Generic2.Dictionary<string, string>();
            test.TrimExcess();
        }

        public static void Test4()
        {
            var dictionary = new System.Collections.Generic2.Dictionary<string, string>();
            Debug.Assert(0 == dictionary.EnsureCapacity(0), "0 == dictionary.EnsureCapacity(0)");
            dictionary.TrimExcess();
            Debug.Assert(0 == dictionary.EnsureCapacity(0), "0 == dictionary.EnsureCapacity(0)");
        }

        public static void Test3()
        {
            var dictionary = new System.Collections.Generic2.Dictionary<string, string>();
            Debug.Assert(dictionary.EnsureCapacity(0) == 0, "dictionary.EnsureCapacity(0)");
        }

        public static void Test2()
        {
            System.Collections.Generic2.Dictionary<string, string> dictionary;

            foreach (var currentCapacity in new int[] { 3, 7 })
            {
                // assert capacity remains the same when ensuring a capacity smaller or equal than existing
                for (int i = 0; i <= currentCapacity; i++)
                {
                    dictionary = new System.Collections.Generic2.Dictionary<string, string>(currentCapacity);
                    Debug.Assert(currentCapacity == dictionary.EnsureCapacity(i), "currentCapacity == dictionary.EnsureCapacity(i)");
                }
            }
        }

        public static void Test()
        {
            System.Collections.Generic2.Dictionary<string, string> test = new System.Collections.Generic2.Dictionary<string, string>();

            Debug.Assert(test.Count == 0, "test.Count");
            Debug.Assert(System.Linq.Enumerable.ToArray(test).Length == 0, "System.Linq.Enumerable.ToArray(test).Length == 0");

            foreach (KeyValuePair<string, string> item in test)
            {
                Debug.Assert(false, "false 1");
            }

            try
            {
                var key = test["Key"];
                Debug.Assert(false, "false 2");
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
                Debug.Assert(test[key] == key, "test[key] == key");
            }

            Debug.Assert(keys.Count == test.Count, "keys.Count == test.Count");

            foreach (KeyValuePair<string, string> item in System.Linq.Enumerable.ToArray(test))
            {
                test.Remove(item.Key);
            }

            Debug.Assert(test.Count == 0, "test.Count == 0");

            test.Clear();

            Debug.Assert(test.Count == 0, "test.Count == 0 2");
        }

    }
}
