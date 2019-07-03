using System;
using System.Diagnostics;

namespace Interview.ArrayAndString
{
    public class ArrayAndString_HashTable
    {
        public static void TestHashTable()
        {
            HashTable<string, string> h = new HashTable<string, string>();
            for (int i = 0; i < 100; i++)
            {
                h.Add(i.ToString(), i.ToString());
            }

            for (int i = 0; i < 100; i++)
            {
                Debug.Assert(h[i.ToString()] == i.ToString());
            }

            try
            {
                var fail = h["100"];
                Debug.Fail("100 found!");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Not found");
            }

            Debug.Assert(h["99"] == "99");
            h.Remove("99");
            try
            {
                var fail = h["99"];
                Debug.Fail("99 found!");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Not found");
            }

            for (int i = 0; i < 100; i++)
            {
                if (i == 99)
                    continue;

                Debug.Assert(h[i.ToString()] == i.ToString());
            }
        }
    }

    public class HashTable<TKey, TValue>
    {
        private Node<TKey, TValue>[] _buckets;
        private int _count = 0;

        public HashTable()
        {
            _buckets = new Node<TKey, TValue>[GeneratePrimes(0)];
        }

        public void Add(TKey key, TValue value)
        {
            if (_buckets.Length < (_count + 1))
            {
                ExpandAndRehash();
            }

            InternalAdd(key, value, _buckets);
            _count++;
        }

        private void ExpandAndRehash()
        {
            Node<TKey, TValue>[] tmp = _buckets;
            _buckets = new Node<TKey, TValue>[GeneratePrimes(_buckets.Length)];

            foreach (Node<TKey, TValue> head in tmp)
            {
                if (head is null)
                {
                    continue;
                }

                Node<TKey, TValue> currentNode = head;
                while (currentNode != null)
                {
                    InternalAdd(currentNode.Key, currentNode.Value, _buckets);
                    currentNode = currentNode.Next;
                }
            }

        }

        private void InternalAdd(TKey key, TValue value, Node<TKey, TValue>[] buckets)
        {
            int bucketIndex = (key.GetHashCode() & 0x7FFFFFFF) % _buckets.Length;

            if (buckets[bucketIndex] is null)
            {
                buckets[bucketIndex] = new Node<TKey, TValue>()
                {
                    Key = key,
                    Value = value
                };
            }
            else
            {
                buckets[bucketIndex] = new Node<TKey, TValue>()
                {
                    Key = key,
                    Value = value,
                    Next = buckets[bucketIndex]
                };
                buckets[bucketIndex].Parent = buckets[bucketIndex];
            }
        }

        private TValue GetValue(TKey key)
        {
            int bucketIndex = (key.GetHashCode() & 0x7FFFFFFF) % _buckets.Length;
            Node<TKey, TValue> currentNode = _buckets[bucketIndex];

            while (currentNode != null)
            {
                if (currentNode.Key.Equals(key))
                {
                    return currentNode.Value;
                }
                currentNode = currentNode.Next;
            }

            throw new InvalidOperationException("Not found");
        }

        public void Remove(TKey key)
        {
            int bucketIndex = (key.GetHashCode() & 0x7FFFFFFF) % _buckets.Length;
            Node<TKey, TValue> currentNode = _buckets[bucketIndex];

            while (currentNode != null)
            {
                if (currentNode.Key.Equals(key))
                {
                    if (currentNode.Parent is null)
                    {
                        _buckets[bucketIndex] = currentNode.Next;
                    }
                    else
                    {
                        currentNode.Parent.Next = currentNode.Next;
                    }
                    currentNode = null;
                }
                else
                {
                    currentNode = currentNode.Next;
                }
            }
        }

        // A prime number (or a prime) is a natural number greater than 1 that cannot be formed by multiplying two smaller natural numbers
        // https://whatis.techtarget.com/definition/prime-number
        private int GeneratePrimes(int greatherThan)
        {
            int n = greatherThan < 1 ? 1 : greatherThan;
            while (true)
            {
                n++;
                if (IsPrime(n))
                {
                    return n;
                }
            }

            static bool IsPrime(int n)
            {
                for (int x = 2; x <= Math.Floor(Math.Sqrt(n)); x++)
                {
                    if (n % x == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public TValue this[TKey key]
        {
            get { return GetValue(key); }
            set { InternalAdd(key, value, _buckets); }
        }

        class Node<TKeyNode, TValueNode>
        {
            public TKeyNode Key { get; set; }
            public TValueNode Value { get; set; }
            public Node<TKeyNode, TValueNode> Parent { get; set; }
            public Node<TKeyNode, TValueNode> Next { get; set; }
        }
    }
}
