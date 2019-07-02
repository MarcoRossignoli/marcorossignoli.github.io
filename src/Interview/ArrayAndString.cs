using System;
using System.Collections;
using System.Collections.Generic;

namespace Interview.ArrayAndString
{
    public class ArrayAndString
    {
        public static void TestArrayList()
        {
            ArrayList<string> list = new ArrayList<string>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i.ToString());
            }

            list.Remove("5");

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------");

            list.Remove("3");

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------");

            list.Add("23");

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class ArrayList<T> : IEnumerable<T>
    {
        private int _defaultSize = 4;
        private T[] _items;
        private int _size = 0;

        public ArrayList()
        {
            _items = new T[_defaultSize];
        }

        public void Add(T value)
        {
            EnsureCapacity(_size + 1);
            _items[_size] = value;
            _size++;
        }

        public void Remove(T value)
        {
            int index = -1;
            for (int i = 0; i < _size; i++)
            {
                if (_items[i].Equals(value))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                return;
            }

            _size--;

            Array.Copy(_items, index + 1, _items, index, _size - index);

            _items[_size] = default;
        }

        public void EnsureCapacity(int size)
        {
            if (_items.Length < size)
            {
                var tmp = _items;
                _items = new T[_items.Length * 2];
                Array.Copy(tmp, _items, _size);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(_items, _size);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Enumerator<T> : IEnumerator<T>
    {
        private T[] _list;
        private int _size;
        private T _currentItem;
        private int _currentIndex;

        public Enumerator(T[] list, int size)
        {
            _size = size;
            _list = list;
        }

        public T Current => _currentItem;

        object IEnumerator.Current => Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (_currentIndex < _size)
            {
                _currentItem = _list[_currentIndex];
                _currentIndex++;

                return true;
            }

            return false;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }
    }
}
