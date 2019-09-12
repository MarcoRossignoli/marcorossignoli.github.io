using System;

namespace Interview
{
    public class StacksAndQueue
    {
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
                if (_head is null)
                {
                    _head = new Node(value, value);
                }
                else
                {
                    Node newNode = new Node(value, Math.Min(value, _head.Min));
                    newNode.Next = _head;
                    _head = newNode;
                }
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
