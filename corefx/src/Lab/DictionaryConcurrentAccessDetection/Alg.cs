using System;
using System.Collections;
using System.Collections.Generic;

namespace DictionaryConcurrentAccessDetection
{
    public class Alg
    {
        public static void TestAlg()
        {
            Entry[] entries = new Entry[3]
          {
                //new Entry(){ hashCode = 1, key = 1, next = -1 },
                //new Entry(){ hashCode = 0, key = 0, next = 0 },
                //new Entry(){ hashCode = 0, key = 0, next = 0 },

                new Entry(){ hashCode = 0, key = 0, next = 0 },
                new Entry(){ hashCode = 0, key = 0, next = 0 },
                new Entry(){ hashCode = 0, key = 0, next = 0 }
          };

            int[] _buckets = new int[3] { 0, 1, 0 };

            int key = 1;
            int value = 1;
            IEqualityComparer comparer = null;
            int hashCode = ((comparer == null) ? key.GetHashCode() : comparer.GetHashCode(key)) & 0x7FFFFFFF;
            int collisionCount = 0;
            ref int bucket = ref _buckets[hashCode % _buckets.Length];
            int i = bucket - 1;
            // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
            do
            {
                // Should be a while loop https://github.com/dotnet/coreclr/issues/15476
                // Test uint in if rather than loop condition to drop range check for following array access
                if ((uint)i >= (uint)entries.Length)
                {
                    break;
                }

                if (entries[i].hashCode == hashCode && EqualityComparer<int>.Default.Equals(entries[i].key, key))
                {
                    //if (behavior == InsertionBehavior.OverwriteExisting)
                    //{
                    //    entries[i].value = value;
                    //    return true;
                    //}

                    //if (behavior == InsertionBehavior.ThrowOnExisting)
                    //{
                    //    ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
                    //}

                    //return false;
                }

                i = entries[i].next;
                if (collisionCount >= entries.Length)
                {
                    // The chain of entries forms a loop; which means a concurrent update has happened.
                    // Break out of the loop and throw, rather than looping forever.
                    throw new Exception("loop");
                }
                collisionCount++;
            } while (true);
        }
    }

    internal class Entry
    {
        internal int hashCode;
        internal int key;
        internal int next;
    }
}
