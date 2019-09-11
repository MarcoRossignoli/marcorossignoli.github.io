using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    public class StacksAndQueue
    {
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
