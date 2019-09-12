using System;
using System.Collections.Generic;

namespace Interview
{
    public class StacksAndQueue
    {
        public static void StackOfPlates()
        {
            StackOfPlatesType t = new StackOfPlatesType(3);

            // Console.WriteLine(t.Pop());

            t.Push(1);
            t.Push(2);
            t.Push(3);
            t.Push(4);
            Console.WriteLine("Peek " + t.Peek());
            t.Push(5);
            t.Push(6);

            while (!t.IsEmpty())
            {
                Console.WriteLine(t.Pop());
            }

            Console.WriteLine();
            t.Push(1);
            t.Push(2);
            t.Push(3);
            t.Push(4);
            t.Push(5);
            t.Push(6);

            Console.WriteLine("Pop at " + t.PopAt(4));
            Console.WriteLine();

            while (!t.IsEmpty())
            {
                Console.WriteLine(t.Pop());
            }

            Console.WriteLine();
            t.Push(1);
            t.Push(2);
            t.Push(3);
            while (!t.IsEmpty())
            {
                Console.WriteLine(t.Pop());
            }
        }

        class StackOfPlatesType
        {
            List<int[]> _arrList = new List<int[]>();
            int _index = -1;
            int _capacity = 0;

            public StackOfPlatesType(int capacity)
            {
                _capacity = capacity;
            }

            public int GetArrayIndex(int index)
            {
                return index / _capacity;
            }

            public int GetIndexIntoArray(int index)
            {
                return index % _capacity;
            }

            public void Push(int val)
            {
                _index++;
                int arr = GetArrayIndex(_index);
                if (arr + 1 > _arrList.Count)
                    _arrList.Add(new int[_capacity]);

                _arrList[arr][GetIndexIntoArray(_index)] = val;
            }

            public int PopAt(int index)
            {
                if (_index == -1)
                    throw new Exception("empty");

                var v = _arrList[GetArrayIndex(index)][GetIndexIntoArray(index)];

                ShiftLeft(index);

                _index--;

                return v;
            }

            public void ShiftLeft(int index)
            {
                while (index < _index)
                {
                    _arrList[GetArrayIndex(index)][GetIndexIntoArray(index)] =
                        _arrList[GetArrayIndex(index + 1)][GetIndexIntoArray(index + 1)];
                    index++;
                }
            }

            public int Pop()
            {
                return PopAt(_index);
            }

            public bool IsEmpty() => _index == -1;

            public int Peek()
            {
                if (_index == -1)
                    throw new Exception("empty");

                return _arrList[GetArrayIndex(_index)][GetIndexIntoArray(_index)];
            }
        }

        public static void QueueNodes()
        {
            // QueueNodesType t = new QueueNodesType();
            QueueFirstLast t = new QueueFirstLast();

            t.Enqueue(1);
            t.Enqueue(2);
            t.Enqueue(3);
            t.Enqueue(4);

            Console.WriteLine(t.Dequeue());
            Console.WriteLine(t.Dequeue());
            Console.WriteLine(t.Dequeue());
            Console.WriteLine(t.Dequeue());
            Console.WriteLine(t.Dequeue());
        }

        class QueueFirstLast
        {
            Node _first; //first to exit
            Node _last; // last inserted

            public void Enqueue(int v)
            {
                Node n = new Node(v);

                if (_last != null)
                {
                    _last.Next = n; // append old last to new last
                }

                _last = n; // set new enqued node as last

                if (_first is null) // set first if null
                {
                    _first = _last;
                }
            }

            public int Dequeue()
            {
                if (_first is null)
                    throw new Exception("empty");

                int val = _first.Val;
                _first = _first.Next;

                if (_first is null)
                {
                    _last = null;
                }

                return val;
            }

            class Node
            {
                public Node(int val) => Val = val;
                public int Val { get; set; }
                public Node Next { get; set; }
            }
        }

        class QueueNodesType
        {
            Node _head;
            Node _tail;

            public void Enqueue(int v)
            {
                Node n = new Node(v);
                if (_head is null)
                {
                    _tail = _head = n;
                }
                else
                {
                    _head = _head.Next = n;
                }
            }

            public int Dequeue()
            {
                if (_tail is null)
                    throw new Exception("empty");

                int val = _tail.Val;
                _tail = _tail.Next;

                if (_tail is null)
                    _head = null;

                return val;
            }

            class Node
            {
                public Node(int val) => Val = val;
                public int Val { get; set; }
                public Node Next { get; set; }
            }
        }

        public static void StackWithArray()
        {
            StackArray sa = new StackArray();
            sa.Push(1);
            sa.Push(2);
            sa.Push(3);
            sa.Push(4);
            Console.WriteLine(sa.Count());
            Console.WriteLine();
            while (!sa.IsEmpty())
            {
                Console.WriteLine(sa.Pop());
            }
        }

        class StackArray
        {
            int[] array = new int[100];
            int _currentIndex = -1;

            public void Push(int v)
            {
                array[++_currentIndex] = v;
            }

            public int Pop()
            {
                if (_currentIndex == -1)
                    throw new Exception("Empty");

                return array[_currentIndex--];
            }

            public bool IsEmpty() => _currentIndex == -1;

            public int Peek()
            {
                if (_currentIndex == -1)
                    throw new Exception("Empty");

                return array[_currentIndex];
            }

            public int Count()
            {
                if (_currentIndex == -1)
                    throw new Exception("Empty");

                return _currentIndex + 1;
            }
        }

        public static void StackMin_Pg99()
        {
            StackMin2 sm = new StackMin2();
            sm.Push(1);
            sm.Push(3);
            sm.Push(2);
            sm.Push(0);

            Console.WriteLine(sm.Min());
            Console.WriteLine(sm.Pop());
            Console.WriteLine(sm.Min());
        }

        class StackMin2
        {
            Node2 _head;
            Node2 _min;

            public int Pop()
            {
                if (_head is null)
                    throw new Exception("Empty");

                int val = _head.Value;

                if (val == _min.Value)
                {
                    _min = _min.Next;
                }

                _head = _head.Next;
                return val;
            }

            public int Min()
            {
                if (_min is null)
                    throw new Exception("Empty");

                return _min.Value;
            }

            public void Push(int value)
            {
                if (_min is null)
                {
                    _min = new Node2(value);
                }
                else
                {
                    if (value <= _min.Value)
                    {
                        var newMin = new Node2(value);
                        newMin.Next = _min;
                        _min = newMin;
                    }
                }

                Node2 newNode = new Node2(value);
                newNode.Next = _head;
                _head = newNode;
            }

            class Node2
            {
                public Node2(int value) => (Value) = (value);
                public int Value { get; set; }
                public Node2 Next { get; set; }
            }
        }

        public static void StackMin_Pg98()
        {
            StackMin sm = new StackMin();
            sm.Push(1);
            sm.Push(3);
            sm.Push(2);
            sm.Push(0);

            Console.WriteLine(sm.Min());
            Console.WriteLine(sm.Pop());
            Console.WriteLine(sm.Min());

        }

        class StackMin
        {
            Node _head;

            public void Print()
            {
                Node r = _head;
                while (r != null)
                {
                    Console.WriteLine(r.Value);
                    r = r.Next;
                }
                Console.WriteLine("end stack");
                Console.WriteLine();
            }

            public int Pop()
            {
                if (_head is null)
                    throw new Exception("Empty");

                int val = _head.Value;
                _head = _head.Next;
                return val;
            }

            public int Min()
            {
                if (_head is null)
                    throw new Exception("Empty");

                return _head.Min;
            }

            public void Push(int value)
            {
                Node newNode = new Node(value, Math.Min(value, _head.Min));
                newNode.Next = _head;
                _head = newNode;
            }

            class Node
            {
                public int Min { get; set; }
                public Node(int value, int min) => (Value, Min) = (value, min);

                public int Value { get; set; }
                public Node Next { get; set; }
            }
        }

        public static void TreeStack()
        {
            int[] array = new int[100];

            ThreeStack<int> first = new ThreeStack<int>(0, 3, array);
            ThreeStack<int> second = new ThreeStack<int>(1, 3, array);
            ThreeStack<int> third = new ThreeStack<int>(2, 3, array);

            first.Push(1);
            first.Push(2);
            first.Push(3);

            Drain(first);

            second.Push(4);
            second.Push(5);
            second.Push(6);

            Drain(second);

            third.Push(7);
            third.Push(8);
            third.Push(9);

            Drain(third);

            first.Push(1);
            first.Push(2);
            first.Push(3);

            Drain(first);

            static void Drain(ThreeStack<int> stack)
            {
                while (!stack.IsEmpty())
                {
                    Console.WriteLine(stack.Pop());
                }
                Console.WriteLine();
            }

        }

        class ThreeStack<T>
        {
            int _count;
            int _currentIndex;
            int _numOfStacks;
            T[] _array;

            public delegate void ExpandDelegate();
            public event ExpandDelegate Expand;

            public ThreeStack(int currentIndex, int numOfStacks, T[] array)
            {
                _numOfStacks = numOfStacks;
                _currentIndex = currentIndex;
                _array = array;
            }

            public void Push(T item)
            {
                if (_count > 0)
                {
                    _currentIndex += _numOfStacks;
                }

                if (_currentIndex > _array.Length - 1)
                    RaiseExpand();

                _array[_currentIndex] = item;
                _count++;
            }

            public T Pop()
            {
                if (_count == 0)
                    return default;

                T item = _array[_currentIndex];
                _count--;

                if (_count > 0)
                    _currentIndex -= _numOfStacks;

                return item;
            }

            public T Peek()
            {
                if (_count == 0)
                    return default;

                return _array[_currentIndex];
            }

            public bool IsEmpty()
            {
                return _count == 0;
            }

            private void RaiseExpand() { this?.Expand(); }
        }
    }
}
