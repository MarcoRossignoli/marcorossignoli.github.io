using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
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
        [Fact]
        public static void Add_DictionaryConcurrentAccessDetection_NullComparer_ValueTypeKey()
        {
            Thread customThread = new Thread(() =>
            {
                Dictionary<int, int> dic = new Dictionary<int, int>();
                dic.Add(1, 1);

                //break internal state
                var entriesType = dic.GetType().GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance);
                var entriesInstance = (Array)entriesType.GetValue(dic);
                var field = entriesInstance.GetType().GetElementType();
                var entryArray = (Array)Activator.CreateInstance(entriesInstance.GetType(), new object[] { dic.Count });
                var entry = Activator.CreateInstance(field);
                entriesType.SetValue(dic, entryArray);

                Assert.Null(dic.GetType().GetField("_comparer", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(dic));
                Assert.True(dic.GetType().GetGenericArguments()[0].IsValueType);
                Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => dic.Add(1, 1)).TargetSite.Name);
                Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => dic[1]).TargetSite.Name);
                //Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => dic.Remove(1)).TargetSite.Name);
            });
            customThread.Start();

            //Wait max 5 seconds, could loop forever
            Assert.True(customThread.Join(TimeSpan.FromSeconds(5)));
        }

        [Fact]
        public static void Add_DictionaryConcurrentAccessDetection_Comparer_ValueTypeKey()
        {
            Thread customThread = new Thread(() =>
             {
                 Dictionary<int, int> dic = new Dictionary<int, int>(new CustomEqualityComparerInt32ValueType());
                 dic.Add(1, 1);

                 //break internal state
                 var entriesType = dic.GetType().GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance);
                 var entriesInstance = (Array)entriesType.GetValue(dic);
                 var field = entriesInstance.GetType().GetElementType();
                 var entryArray = (Array)Activator.CreateInstance(entriesInstance.GetType(), new object[] { dic.Count });
                 var entry = Activator.CreateInstance(field);
                 entriesType.SetValue(dic, entryArray);

                 Assert.NotNull(dic.GetType().GetField("_comparer", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(dic));
                 Assert.True(dic.GetType().GetGenericArguments()[0].IsValueType);
                 Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => dic.Add(1, 1)).TargetSite.Name);
                 Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => dic[1]).TargetSite.Name);
                 //Assert.Equal("ThrowInvalidOperationException_ConcurrentOperationsNotSupported", Assert.Throws<InvalidOperationException>(() => dic.Remove(1)).TargetSite.Name);
             });
            customThread.Start();

            //Wait max 5 seconds, could loop forever
            Assert.True(customThread.Join(TimeSpan.FromSeconds(5)));
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
