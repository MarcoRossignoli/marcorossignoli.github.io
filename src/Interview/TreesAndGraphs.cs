using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Interview
{
    class TreesAndGraphs
    {
        public static void CreateBinaryTreeFromOrderedArray()
        {
            int[] a = new int[] { 1, 2, 3, 4, 5, 6, 7 };

            BinaryNode d = Create(a, 0, a.Length - 1);

            return;

            static BinaryNode Create(int[] a, int start, int end)
            {
                if (start > end)
                    return null;

                int middle = (start + end) / 2;

                BinaryNode bn = new BinaryNode(a[middle]);

                bn.Left = Create(a, start, middle - 1);
                bn.Right = Create(a, middle + 1, end);

                return bn;
            }
        }

        [DebuggerDisplay("{Left.Val} <- {Val} -> {Right.Val}")]
        class BinaryNode
        {
            public BinaryNode(int val) => Val = val;
            public int Val { get; set; }
            public BinaryNode Left { get; set; }
            public BinaryNode Right { get; set; }
        }

        public static void RouteBetweenNodes()
        {
            DirectedNode a = new DirectedNode() { Val = "A" };
            DirectedNode b = new DirectedNode() { Val = "B" };
            DirectedNode c = new DirectedNode() { Val = "C" };
            DirectedNode d = new DirectedNode() { Val = "D" };
            DirectedNode e = new DirectedNode() { Val = "E" };

            a.Adjacent = new List<DirectedNode>() { c };
            // a.Adjacent = new List<DirectedNode>() { b };
            b.Adjacent = new List<DirectedNode>() { d };
            c.Adjacent = new List<DirectedNode>() { e };
            d.Adjacent = new List<DirectedNode>() { a };
            e.Adjacent = new List<DirectedNode>() { d };

            Console.WriteLine(FoundRoute(a, d));

            return;

            static bool FoundRoute(DirectedNode start, DirectedNode end)
            {
                if (start == end)
                    return true;

                System.Collections.Generic.Queue<DirectedNode> queue = new System.Collections.Generic.Queue<DirectedNode>();
                HashSet<DirectedNode> hash = new HashSet<DirectedNode>();
                hash.Add(start);

                queue.Enqueue(start);

                while (queue.Count > 0)
                {
                    DirectedNode node = queue.Dequeue();
                    if (!hash.Contains(node))
                    {
                        if (node == end)
                            return true;

                        hash.Add(node);
                    }

                    foreach (DirectedNode adj in node.Adjacent)
                    {
                        if (!hash.Contains(adj))
                            queue.Enqueue(adj);
                    }
                }

                return false;
            }
        }

        public static void GraphSearch()
        {
            Console.WriteLine("DepthFirstSearchRecoursion");
            DepthFirstSearchRecoursion(New());

            Console.WriteLine("DepthFirstSearchStack");
            DepthFirstSearchStack(New());
            Console.WriteLine("BreadthFirstSearchQueue");
            BreadthFirstSearchQueue(New());

            return;

            static void BreadthFirstSearchQueue(DirectedNode node)
            {
                if (node is null)
                    return;

                System.Collections.Generic.Queue<DirectedNode> s = new System.Collections.Generic.Queue<DirectedNode>();
                s.Enqueue(node);


                while (s.Count > 0)
                {
                    DirectedNode n = s.Dequeue();

                    if (!n.Explored)
                    {
                        Visit(n);
                        n.Explored = true;
                    }

                    foreach (DirectedNode n2 in n.Adjacent)
                    {
                        if (!n2.Explored)
                        {
                            s.Enqueue(n2);
                        }
                    }
                }
            }

            static void DepthFirstSearchRecoursion(DirectedNode node)
            {
                if (node is null)
                    return;

                node.Explored = true;
                Visit(node);

                foreach (DirectedNode n in node.Adjacent)
                {
                    if (!n.Explored)
                    {
                        DepthFirstSearchRecoursion(n);
                    }
                }

            }

            static void DepthFirstSearchStack(DirectedNode node)
            {
                if (node is null)
                    return;

                System.Collections.Generic.Stack<DirectedNode> s = new System.Collections.Generic.Stack<DirectedNode>();
                s.Push(node);


                while (s.Count > 0)
                {
                    DirectedNode n = s.Pop();

                    if (!n.Explored)
                    {
                        Visit(n);
                        n.Explored = true;
                    }

                    foreach (DirectedNode n2 in n.Adjacent)
                    {
                        if (!n2.Explored)
                        {
                            s.Push(n2);
                        }
                    }
                }
            }

            static void Visit(DirectedNode node)
            {
                Console.WriteLine(node.Val);
            }

            static DirectedNode New()
            {
                var d = new DirectedNode() { Val = "D" };
                return new DirectedNode()
                {
                    Val = "A",
                    Adjacent = new List<DirectedNode>()
                {
                    d,
                    new DirectedNode(){Val = "B", Adjacent = new List<DirectedNode>()
                    {
                        new DirectedNode(){Val = "C"},
                        new DirectedNode(){Val = "E", Adjacent = new List<DirectedNode>() { d } }
                    }
                    },
                }
                };
            }
        }

        [DebuggerDisplay("Val = {Val }Explored = {Explored}")]
        class DirectedNode
        {
            public bool Explored { get; set; }
            public string Val { get; set; }
            public List<DirectedNode> Adjacent { get; set; } = new List<DirectedNode>();
        }

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
