using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    class TreesAndGraphs
    {
        public static void BinaryMinHeaps()
        {
            BinaryMinHeap bmh = new BinaryMinHeap(10);
            bmh.Add(1);
            bmh.Add(2);
            bmh.Add(3);
            bmh.Add(4);
            bmh.Add(5);
            bmh.Add(-1);
            bmh.Poll();
        }

        // https://www.youtube.com/watch?v=t0Cq6tVNRBA
        class BinaryMinHeap
        {
            int[] _items;
            int _size = 0;

            public BinaryMinHeap(int capacity) { _items = new int[capacity]; }

            private int getLeftIndexChild(int index) => 2 * index + 1;
            private int getRightIndexChild(int index) => 2 * index + 2;
            private int getParentIndex(int index) => (index - 1) / 2;

            private bool hasLeftChild(int index) => getLeftIndexChild(index) < _size;
            private bool hasRightChild(int index) => getRightIndexChild(index) < _size;
            private bool hasParent(int index) => getParentIndex(index) >= 0;

            private int leftChild(int index) => _items[getLeftIndexChild(index)];
            private int rightChild(int index) => _items[getRightIndexChild(index)];
            private int getParent(int index) => _items[getParentIndex(index)];

            private void Swap(int indexOne, int indexTwo)
            {
                int tmp = _items[indexOne];
                _items[indexOne] = _items[indexTwo];
                _items[indexTwo] = tmp;
            }

            private void EnsureCapacity()
            {
                if (_size == _items.Length)
                {
                    int[] items = new int[_items.Length * 2];
                    Array.Copy(_items, 0, items, 0, _items.Length);
                    _items = items;
                }
            }

            public int Peek()
            {
                if (_size == 0)
                    throw new Exception("empty");
                return _items[0];
            }

            public int Poll()
            {
                if (_size == 0)
                    throw new Exception("empty");

                int item = _items[0];
                _items[0] = _items[_size - 1];
                _size--;
                heapifyDown();
                return item;
            }

            public void Add(int item)
            {
                EnsureCapacity();
                _items[_size] = item;
                _size++;
                heapifyUp();
            }

            private void heapifyUp()
            {
                int index = _size - 1;
                while (hasParent(index) && getParent(index) > _items[index])
                {
                    Swap(getParentIndex(index), index);
                    index = getParentIndex(index);
                }
            }

            private void heapifyDown()
            {
                int index = 0;
                while (hasLeftChild(index))
                {
                    int smallerChildIndex = getLeftIndexChild(index);
                    if (hasRightChild(index) && rightChild(index) < leftChild(index))
                    {
                        smallerChildIndex = getRightIndexChild(index);
                    }

                    if (_items[index] < _items[smallerChildIndex])
                    {
                        break;
                    }
                    else
                    {
                        Swap(index, smallerChildIndex);
                    }

                    index = smallerChildIndex;
                }
            }

        }
    }
}
