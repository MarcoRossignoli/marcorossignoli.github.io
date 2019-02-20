using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Dic;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            D();
            Test();
            Test2();
            Test3();
            Test4();
            Test5();
            Test6();
            Test7();
            Test8();
            Test9();
            Test10();
            Test11();
        }

        public static void D()
        {
            var dictionary = new System.Collections.Generic2.Dictionary<string, int>();
            dictionary.Add("System.Runtime.InteropServices.FieldOffsetAttribute", 0);
            dictionary.Add("System.Runtime.InteropServices.MarshalAsAttribute", 0);
            dictionary.Add("System.Runtime.InteropServices.ComImportAttribute", 0);
            dictionary.Add("System.NonSerializedAttribute", 0);
            dictionary.Add("System.Runtime.InteropServices.InAttribute", 0);
            dictionary.Add("System.Runtime.InteropServices.OutAttribute", 0);
            dictionary.Add("System.Runtime.InteropServices.OptionalAttribute", 0);
            dictionary.Add("System.Runtime.InteropServices.DllImportAttribute", 0);
            dictionary.Add("System.Runtime.InteropServices.PreserveSigAttribute", 0);
            dictionary.Add("System.Runtime.CompilerServices.TypeForwardedToAttribute", 0);
            dictionary.Add("xunit.console/99.99.99-dev", 0);
        }

        public static void Test11()
        {
            foreach (var item in CopyConstructorStringData)
            {
                int size = (int)item[0];
                Func<int, string> keyValueSelector = (Func<int, string>)item[1];
                Func<IDictionary<string, string>, IDictionary<string, string>> dictionarySelector = (Func<IDictionary<string, string>, IDictionary<string, string>>)item[2];

                TestCopyConstructor(size, keyValueSelector, dictionarySelector);
            }
        }

        public static void Test10()
        {
            foreach (var item in CopyConstructorInt32Data)
            {
                int size = (int)item[0];
                Func<int, int> keyValueSelector = (Func<int, int>)item[1];
                Func<IDictionary<int, int>, IDictionary<int, int>> dictionarySelector = (Func<IDictionary<int, int>, IDictionary<int, int>>)item[2];

                TestCopyConstructor(size, keyValueSelector, dictionarySelector);
            }
        }

        private static void TestCopyConstructor<T>(int size, Func<int, T> keyValueSelector, Func<IDictionary<T, T>, IDictionary<T, T>> dictionarySelector)
        {
            IDictionary<T, T> expected = CreateDictionary(size, keyValueSelector);
            IDictionary<T, T> input = dictionarySelector(CreateDictionary(size, keyValueSelector));

            var newDic = new System.Collections.Generic2.Dictionary<T, T>(input);
            Assert.Equal(expected, newDic);
        }

        public static IEnumerable<object[]> CopyConstructorStringData
        {
            get { return GetCopyConstructorData(i => i.ToString()); }
        }

        public static IEnumerable<object[]> CopyConstructorInt32Data
        {
            get { return GetCopyConstructorData(i => i); }
        }

        private static IDictionary<T, T> CreateDictionary<T>(int size, Func<int, T> keyValueSelector, IEqualityComparer<T> comparer = null)
        {
            System.Collections.Generic2.Dictionary<T, T> dict = Enumerable.Range(0, size + 1).ToDictionary(keyValueSelector, keyValueSelector, comparer);
            // Remove first item to reduce Count to size and alter the contiguity of the dictionary
            dict.Remove(keyValueSelector(0));
            return dict;
        }

        private static IEnumerable<object[]> GetCopyConstructorData<T>(Func<int, T> keyValueSelector, IEqualityComparer<T>[] comparers = null)
        {
            var dictionarySelectors = new Func<IDictionary<T, T>, IDictionary<T, T>>[]
            {
                d => d,
                d => new DictionarySubclass<T, T>(d),
                d => new ReadOnlyDictionary<T, T>(d)
            };

            var sizes = new int[] 
            { 
                // 0, 
                1, 
                // 2, 
                // 3 
            };

            foreach (Func<IDictionary<T, T>, IDictionary<T, T>> dictionarySelector in dictionarySelectors)
            {
                foreach (int size in sizes)
                {
                    if (comparers != null)
                    {
                        foreach (IEqualityComparer<T> comparer in comparers)
                        {
                            yield return new object[] { size, keyValueSelector, dictionarySelector, comparer };
                        }
                    }
                    else
                    {
                        yield return new object[] { size, keyValueSelector, dictionarySelector };
                    }
                }
            }
        }

        public static void Test9()
        {
            foreach (var count in new int[] { 85, 89 })
            {
                var dictionary = new System.Collections.Generic2.Dictionary<int, int>(20);
                for (int i = 0; i < count; i++)
                {
                    dictionary.Add(i, 0);
                }
                dictionary.TrimExcess();
                var res = dictionary.EnsureCapacity(0);
                Debug.Assert(res >= count && res <= int.MaxValue);
            }
        }

        public static void Test8()
        {
            System.Collections.Generic2.Dictionary<string, int> dictionary = new System.Collections.Generic2.Dictionary<string, int>(7);
            for (int i = 0; i < 4; i++)
            {
                dictionary.Add(i.ToString(), 0);
            }
            var s_64bit = new string[] { "95e85f8e-67a3-4367-974f-dd24d8bb2ca2", "eb3d6fe9-de64-43a9-8f58-bddea727b1ca" };
            var s_32bit = new string[] { "25b1f130-7517-48e3-96b0-9da44e8bfe0e", "ba5a3625-bc38-4bf1-a707-a3cfe2158bae" };
            string[] chained = (Environment.Is64BitProcess ? s_64bit : s_32bit).ToArray();
            dictionary.Add(chained[0], 0);
            dictionary.Add(chained[1], 0);
            for (int i = 0; i < 4; i++)
            {
                dictionary.Remove(i.ToString());
            }
            dictionary.TrimExcess(3);
            Debug.Assert(2 == dictionary.Count);
            int val;
            Debug.Assert(dictionary.TryGetValue(chained[0], out val));
            Debug.Assert(dictionary.TryGetValue(chained[1], out val));
        }

        public static void Test7()
        {
            System.Collections.Generic2.Dictionary<string, string> test = new System.Collections.Generic2.Dictionary<string, string>();
            test.Add("A", "A");
            test.Add("B", "B");
            test.Add("C", "C");
            using (var enumerator = test.GetEnumerator())
            {
                enumerator.MoveNext();
                enumerator.MoveNext();
                test.Remove(enumerator.Current.Key);
                Debug.Assert(enumerator.MoveNext());
                Debug.Assert(!enumerator.MoveNext());
            }
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

        private sealed class DictionarySubclass<TKey, TValue> : System.Collections.Generic2.Dictionary<TKey, TValue>
        {
            public DictionarySubclass(IDictionary<TKey, TValue> dictionary)
            {
                foreach (var pair in dictionary)
                {
                    Add(pair.Key, pair.Value);
                }
            }
        }
    }

    public static partial class MyEnumerable
    {
        public static System.Collections.Generic2.Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
            ToDictionary(source, keySelector, null);

        public static System.Collections.Generic2.Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (keySelector == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keySelector);
            }

            int capacity = 0;
            if (source is ICollection<TSource> collection)
            {
                capacity = collection.Count;
                if (capacity == 0)
                {
                    return new System.Collections.Generic2.Dictionary<TKey, TSource>(comparer);
                }

                if (collection is TSource[] array)
                {
                    return ToDictionary(array, keySelector, comparer);
                }

                if (collection is List<TSource> list)
                {
                    return ToDictionary(list, keySelector, comparer);
                }
            }

            System.Collections.Generic2.Dictionary<TKey, TSource> d = new System.Collections.Generic2.Dictionary<TKey, TSource>(capacity, comparer);
            foreach (TSource element in source)
            {
                d.Add(keySelector(element), element);
            }

            return d;
        }

        private static System.Collections.Generic2.Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(TSource[] source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            System.Collections.Generic2.Dictionary<TKey, TSource> d = new System.Collections.Generic2.Dictionary<TKey, TSource>(source.Length, comparer);
            for (int i = 0; i < source.Length; i++)
            {
                d.Add(keySelector(source[i]), source[i]);
            }

            return d;
        }

        private static System.Collections.Generic2.Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(List<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            System.Collections.Generic2.Dictionary<TKey, TSource> d = new System.Collections.Generic2.Dictionary<TKey, TSource>(source.Count, comparer);
            foreach (TSource element in source)
            {
                d.Add(keySelector(element), element);
            }

            return d;
        }

        public static System.Collections.Generic2.Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) =>
            ToDictionary(source, keySelector, elementSelector, null);

        public static System.Collections.Generic2.Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (keySelector == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keySelector);
            }

            if (elementSelector == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementSelector);
            }

            int capacity = 0;
            if (source is ICollection<TSource> collection)
            {
                capacity = collection.Count;
                if (capacity == 0)
                {
                    return new System.Collections.Generic2.Dictionary<TKey, TElement>(comparer);
                }

                if (collection is TSource[] array)
                {
                    return ToDictionary(array, keySelector, elementSelector, comparer);
                }

                if (collection is List<TSource> list)
                {
                    return ToDictionary(list, keySelector, elementSelector, comparer);
                }
            }

            System.Collections.Generic2.Dictionary<TKey, TElement> d = new System.Collections.Generic2.Dictionary<TKey, TElement>(capacity, comparer);
            foreach (TSource element in source)
            {
                d.Add(keySelector(element), elementSelector(element));
            }

            return d;
        }

        private static System.Collections.Generic2.Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(TSource[] source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            System.Collections.Generic2.Dictionary<TKey, TElement> d = new System.Collections.Generic2.Dictionary<TKey, TElement>(source.Length, comparer);
            for (int i = 0; i < source.Length; i++)
            {
                d.Add(keySelector(source[i]), elementSelector(source[i]));
            }

            return d;
        }

        private static System.Collections.Generic2.Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(List<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            System.Collections.Generic2.Dictionary<TKey, TElement> d = new System.Collections.Generic2.Dictionary<TKey, TElement>(source.Count, comparer);
            foreach (TSource element in source)
            {
                d.Add(keySelector(element), elementSelector(element));
            }

            return d;
        }

        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source) => source.ToHashSet(comparer: null);

        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            // Don't pre-allocate based on knowledge of size, as potentially many elements will be dropped.
            return new HashSet<TSource>(source, comparer);
        }
    }
}
