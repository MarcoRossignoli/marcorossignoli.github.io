using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
namespace DictionaryConcurrentAccessDetection
{
    /*          
               
     
     entry.GetType().GetField("next").SetValue(entry, -1);
     entry.GetType().GetField("hashCode").SetValue(entry, 1);
     entry.GetType().GetField("key").SetValue(entry, 1);
     entry.GetType().GetField("value").SetValue(entry, 1);
     entryArray.SetValue(entry, 1);

     private struct Entry
     {
          public int hashCode;    // Lower 31 bits of hash code, -1 if unused
          public int next;        // Index of next entry, -1 if last
          public TKey key;        // Key of entry
          public TValue value;    // Value of entry
     }
   */
    public class Tests
    {
        async static Task DictionaryConcurrentAccessDetection<TKey, TValue>(Dictionary<TKey, TValue> dictionary, bool isValueType, object comparer, Action<Dictionary<TKey, TValue>> add, Action<Dictionary<TKey, TValue>> get, Action<Dictionary<TKey, TValue>> remove, Action<Dictionary<TKey, TValue>> removeOutParam)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                //break internal state
                var entriesType = dictionary.GetType().GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance);
                var entriesInstance = (Array)entriesType.GetValue(dictionary);
                var field = entriesInstance.GetType().GetElementType();
                var entryArray = (Array)Activator.CreateInstance(entriesInstance.GetType(), new object[] { ((IDictionary)dictionary).Count });
                var entry = Activator.CreateInstance(field);
                entriesType.SetValue(dictionary, entryArray);

                Assert.Equal(comparer, dictionary.GetType().GetField("_comparer", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(dictionary));
                Assert.Equal(isValueType, dictionary.GetType().GetGenericArguments()[0].IsValueType);
                Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => add(dictionary)).TargetSite.Name);
                Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => get(dictionary)).TargetSite.Name);
                //Remove is not resilient yet
                //Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => remove(dictionary)).TargetSite.Name);
                //Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => removeOutParam(dictionary)).TargetSite.Name);
            }, TaskCreationOptions.LongRunning);

            //Wait max 60 seconds, could loop forever
            Assert.True((await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(60))) == task) && task.IsCompletedSuccessfully);
        }

        [Fact]
        async public static Task Add_DictionaryConcurrentAccessDetection_NullComparer_ValueTypeKey()
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add(1, 1);
            await DictionaryConcurrentAccessDetection(dic,
                true,
                null,
                d => d.Add(1, 1),
                d => { var v = d[1]; },
                d => d.Remove(1),
                d => d.Remove(1, out int value));
        }

        [Fact]
        async public static Task Add_DictionaryConcurrentAccessDetection_Comparer_ValueTypeKey()
        {
            var comparer = new CustomEqualityComparerInt32ValueType();
            Dictionary<int, int> dic = new Dictionary<int, int>(new CustomEqualityComparerInt32ValueType());
            dic.Add(1, 1);
            await DictionaryConcurrentAccessDetection(dic,
                true,
                comparer,
                d => d.Add(1, 1),
                d => { var v = d[1]; },
                d => d.Remove(1),
                d => d.Remove(1, out int value));
        }

        [Fact]
        async public static Task Add_DictionaryConcurrentAccessDetection_NullComparer_ReferenceTypeKey()
        {
            Dictionary<DummyRefType, DummyRefType> dic = new Dictionary<DummyRefType, DummyRefType>();
            dic.Add(new DummyRefType() { Value = 1 }, new DummyRefType() { Value = 1 });
            await DictionaryConcurrentAccessDetection(dic,
                false,
                null,
                d => d.Add(new DummyRefType() { Value = 1 }, new DummyRefType() { Value = 1 }),
                d => { var v = d[new DummyRefType() { Value = 1 }]; },
                d => d.Remove(new DummyRefType() { Value = 1 }),
                d => d.Remove(new DummyRefType() { Value = 1 }, out DummyRefType value));
        }

        [Fact]
        async public static Task Add_DictionaryConcurrentAccessDetection_Comparer_ReferenceTypeKey()
        {
            var comparer = new CustomEqualityComparerDummyRefType();
            Dictionary<DummyRefType, DummyRefType> dic = new Dictionary<DummyRefType, DummyRefType>(comparer);
            dic.Add(new DummyRefType() { Value = 1 }, new DummyRefType() { Value = 1 });
            await DictionaryConcurrentAccessDetection(dic,
                false,
                comparer,
                d => d.Add(new DummyRefType() { Value = 1 }, new DummyRefType() { Value = 1 }),
                d => { var v = d[new DummyRefType() { Value = 1 }]; },
                d => d.Remove(new DummyRefType() { Value = 1 }),
                d => d.Remove(new DummyRefType() { Value = 1 }, out DummyRefType value));
        }
    }

    public class DummyRefType
    {
        public int Value { get; set; }
        public override bool Equals(object obj)
        {
            return ((DummyRefType)obj).Equals(this.Value);
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    public class CustomEqualityComparerDummyRefType : EqualityComparer<DummyRefType>
    {
        public override bool Equals(DummyRefType x, DummyRefType y)
        {
            return x.Value == y.Value;
        }

        public override int GetHashCode(DummyRefType obj)
        {
            return obj.GetHashCode();
        }
    }

    public class CustomEqualityComparerInt32ValueType : EqualityComparer<int>
    {
        public override bool Equals(int x, int y)
        {
            return EqualityComparer<int>.Default.Equals(x, y);
        }

        public override int GetHashCode(int obj)
        {
            return EqualityComparer<int>.Default.GetHashCode(obj);
        }
    }
}
