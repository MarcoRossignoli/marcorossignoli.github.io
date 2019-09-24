using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using fxLinkedListInt = System.Collections.Generic.LinkedList<int>;
namespace Interview
{
    class TreesAndGraphs
    {
        public static void BTCSequence_Pg263()
        {
            int[] a = new int[] { 1, 2, 3, 4 };
            BinaryNode d = Create(a, 0, a.Length - 1);
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

            var r = allSequence(d);

            foreach (var item in r)
            {
                foreach (var i2 in item)
                {
                    Console.WriteLine(i2);
                }
                Console.WriteLine("----");
            }

            Console.WriteLine("End");

            return;

            static List<fxLinkedListInt> allSequence(BinaryNode b)
            {
                List<fxLinkedListInt> result = new List<fxLinkedListInt>();

                if (b is null)
                {
                    result.Add(new fxLinkedListInt());
                    return result;
                }

                fxLinkedListInt prefix = new fxLinkedListInt();
                prefix.AddLast(b.Val);

                List<fxLinkedListInt> leftSeq = allSequence(b.Left);
                List<fxLinkedListInt> rightSeq = allSequence(b.Right);

                foreach (var l in leftSeq)
                {
                    foreach (var r in rightSeq)
                    {
                        List<fxLinkedListInt> weaved = new List<fxLinkedListInt>();

                        waveList(l, r, weaved, prefix);

                        result.AddRange(weaved);
                    }
                }

                return result;
            }

            static void waveList(fxLinkedListInt first, fxLinkedListInt second, List<fxLinkedListInt> results, fxLinkedListInt prefix)
            {
                if (first.Count == 0 || second.Count == 0)
                {
                    fxLinkedListInt result = Clone(prefix);

                    Debug.Assert(result != prefix);

                    foreach (var i in first)
                    {
                        result.AddLast(i);
                    }

                    foreach (var i in second)
                    {
                        result.AddLast(i);
                    }

                    results.Add(result);
                    return;
                }

                int headFirst = first.First.Value;
                first.RemoveFirst();
                prefix.AddLast(headFirst);
                waveList(first, second, results, prefix);
                prefix.RemoveLast();
                first.AddFirst(headFirst);

                int headSecond = second.First.Value;
                second.RemoveFirst();
                prefix.AddLast(headSecond);
                waveList(first, second, results, prefix);
                prefix.RemoveLast();
                second.AddFirst(headSecond);

            }

            static fxLinkedListInt Clone(fxLinkedListInt l)
            {
                fxLinkedListInt n = new fxLinkedListInt();
                foreach (int item in l)
                {
                    n.AddLast(item);
                }
                return n;
            }

        }

        public static void BTCSequence()
        {
            int[] a = new int[] { 1, 2, 3, 4 };
            BinaryNode d = Create(a, 0, a.Length - 1);
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

            var array = GetArray(d);

            PrintPerm(array, 0, "");

            return;

            static void PrintPerm(int[] array, int level, string parentToPrint)
            {
                int s = (int)Math.Pow(2, level) - 1;
                int e = s + (int)Math.Pow(2, level) - 1;

                if (s > array.Length - 1)
                {
                    Console.WriteLine(parentToPrint);
                    return;
                }

                foreach (var perm in GetPerm(array, s, Math.Min(e, array.Length - 1)))
                {
                    PrintPerm(array, level + 1, parentToPrint + perm);
                }
            }

            static IEnumerable<string> GetPerm(int[] array, int s, int e)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = s; i <= e; i++)
                {
                    builder.Append(array[i]);
                }

                List<string> perm = new List<string>();

                InternalPer(builder.ToString(), "", perm);

                return perm;
            }

            static void InternalPer(string s, string prefix, List<string> perms)
            {
                if (s.Length == 0)
                {
                    perms.Add(prefix);
                    return;
                }
                for (int i = 0; i < s.Length; i++)
                {
                    string remain = s.Substring(0, i) + s.Substring(i + 1);
                    InternalPer(remain, prefix + s[i], perms);
                }
            }

            static int[] GetArray(BinaryNode node)
            {
                if (node is null)
                    return null;
                Queue<BinaryNode> q = new Queue<BinaryNode>();
                q.Add(node);

                List<int> array = new List<int>();

                while (!q.IsEmpty())
                {
                    BinaryNode d = q.Remove();
                    array.Add(d.Val);
                    if (d.Left != null)
                        q.Add(d.Left);
                    if (d.Right != null)
                        q.Add(d.Right);
                }

                return array.ToArray();
            }
        }

        public static void FirstCommonAncestorPostOrder()
        {
            int[] a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            BinaryNodeFound root = Create(a, 0, a.Length - 1);

            BinaryNodeFound root2 = Create(a, 0, a.Length - 1);

            BinaryNodeFound oneToPass = GetNode(root, 8);
            BinaryNodeFound twoToPass = GetNode(root, 1);

            BinaryNodeFound result = FoundFirstAncestor(root, oneToPass, twoToPass).CommonNode;

            Console.WriteLine(result is null ? -1 : result.Val);

            return;

            static ResultFirstCommonAncestorPostOrder FoundFirstAncestor(BinaryNodeFound node, BinaryNodeFound one, BinaryNodeFound two)
            {
                if (node is null)
                    return new ResultFirstCommonAncestorPostOrder() { CurrentSum = 0 };

                if (one == two)
                    return new ResultFirstCommonAncestorPostOrder() { CurrentSum = 0, CommonNode = one };

                ResultFirstCommonAncestorPostOrder l = FoundFirstAncestor(node.Left, one, two);

                if (l.CommonNode != null)
                    return l;

                ResultFirstCommonAncestorPostOrder r = FoundFirstAncestor(node.Right, one, two);
                if (r.CommonNode != null)
                    return r;

                if (l.CurrentSum + r.CurrentSum == 2)
                {
                    return new ResultFirstCommonAncestorPostOrder() { CommonNode = node };
                }

                var currentSum = (node == one || node == two ? 1 : 0) + l.CurrentSum + r.CurrentSum;

                if (currentSum == 2)
                {
                    return new ResultFirstCommonAncestorPostOrder() { CommonNode = node };
                }

                return new ResultFirstCommonAncestorPostOrder() { CurrentSum = currentSum };
            }

            static BinaryNodeFound Create(int[] a, int start, int end)
            {
                if (start > end)
                    return null;

                int middle = (start + end) / 2;

                BinaryNodeFound bn = new BinaryNodeFound(a[middle]);

                bn.Left = Create(a, start, middle - 1);
                bn.Right = Create(a, middle + 1, end);

                return bn;
            }
            static BinaryNodeFound GetNode(BinaryNodeFound node, int val)
            {
                if (node is null)
                    return null;

                BinaryNodeFound l = GetNode(node.Left, val);
                if (l != null)
                    return l;
                if (node.Val == val)
                    return node;
                BinaryNodeFound r = GetNode(node.Right, val);
                if (r != null)
                    return r;

                return null;
            }
        }

        class ResultFirstCommonAncestorPostOrder
        {
            public int CurrentSum { get; set; }
            public BinaryNodeFound CommonNode { get; set; }
        }

        [DebuggerDisplay("{Val}")]
        class BinaryNodeFound
        {
            public bool Found { get; set; }
            public BinaryNodeFound(int val) => Val = val;
            public int Val { get; set; }
            public BinaryNodeFound Left { get; set; }
            public BinaryNodeFound Right { get; set; }
        }


        public static void BuildOrder()
        {
            BinaryNodeLetter a = new BinaryNodeLetter("A");
            BinaryNodeLetter b = new BinaryNodeLetter("B");
            BinaryNodeLetter c = new BinaryNodeLetter("C");
            BinaryNodeLetter d = new BinaryNodeLetter("D");
            BinaryNodeLetter e = new BinaryNodeLetter("E");
            BinaryNodeLetter f = new BinaryNodeLetter("F");

            d.Children.Add(a);
            b.Children.Add(f);
            d.Children.Add(b);
            a.Children.Add(f);
            c.Children.Add(d);
            // d.Children.Add(e);
            // f.Children.Add(c);

            List<BinaryNodeLetter> list = new List<BinaryNodeLetter>() { a, b, c, d, e, f };

            List<BinaryNodeLetter> buildOrder = new List<BinaryNodeLetter>();

            foreach (BinaryNodeLetter n in list)
            {
                if (!Compile(n, buildOrder, list.Count))
                {
                    Debug.WriteLine("Build not possible");
                    return;
                }
            }

            foreach (var n in buildOrder)
            {
                Debug.Write(n.Val);
            }

            return;

            static bool Compile(BinaryNodeLetter node, List<BinaryNodeLetter> buildList, int projectNum)
            {
                if (projectNum < 0)
                    return false;

                foreach (BinaryNodeLetter n1 in node.Children)
                {
                    if (!Compile(n1, buildList, --projectNum))
                        return false;
                }

                if (!buildList.Contains(node))
                    buildList.Add(node);

                return true;
            }

        }

        [DebuggerDisplay("{Val}")]
        class BinaryNodeLetter
        {
            public BinaryNodeLetter(string val) => Val = val;
            public string Val { get; set; }
            public List<BinaryNodeLetter> Children { get; set; } = new List<BinaryNodeLetter>();
        }

        public static void Successor()
        {
            int[] a = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            BinaryNodeWithParent d = Create(a, 0, a.Length - 1, null);
            static BinaryNodeWithParent Create(int[] a, int start, int end, BinaryNodeWithParent parent)
            {
                if (start > end)
                    return null;

                int middle = (start + end) / 2;

                BinaryNodeWithParent bn = new BinaryNodeWithParent(a[middle]) { Parent = parent };

                bn.Left = Create(a, start, middle - 1, bn);
                bn.Right = Create(a, middle + 1, end, bn);

                return bn;
            }

            BinaryNodeWithParent startNode = InOrderGetN(d, 5);
            var res = GetNextNode(startNode);
            Console.WriteLine(res is null ? -1 : res.Val);
            return;

            static BinaryNodeWithParent InOrderGetN(BinaryNodeWithParent node, int val)
            {
                if (node != null)
                {
                    var nl = InOrderGetN(node.Left, val);

                    if (nl != null)
                        return nl;

                    if (val == node.Val)
                        return node;

                    var nr = InOrderGetN(node.Right, val);
                    if (nr != null)
                        return nr;
                }
                return null;
            }
            static BinaryNodeWithParent FirstInOrder(BinaryNodeWithParent node)
            {
                if (node != null)
                {
                    var l = FirstInOrder(node.Left);
                    if (l != null)
                        return l;

                    return node;
                }
                return null;
            }
            static BinaryNodeWithParent GetNextNode(BinaryNodeWithParent node)
            {
                int nodeVal = node.Val;
                if (node.Right is null)
                {
                    while (node.Parent != null)
                    {
                        if (node.Parent.Val > nodeVal)
                        {
                            return node.Parent;
                        }
                        node = node.Parent;
                    }
                    return null;
                }
                else
                {
                    return FirstInOrder(node.Right);
                }
            }
        }


        [DebuggerDisplay("{Left.Val} <- {Val} -> {Right.Val}")]
        class BinaryNodeWithParent
        {
            public BinaryNodeWithParent(int val) => Val = val;
            public BinaryNodeWithParent Parent { get; set; }
            public int Val { get; set; }
            public BinaryNodeWithParent Left { get; set; }
            public BinaryNodeWithParent Right { get; set; }
        }

        public static void IsBstMinMaxBest()
        {
            // int[] a = new int[] { 6, 7, 8, 10, 5, 12, 13 };
            int[] a = new int[] { 6, 7, 8, 10, 11, 12, 13 };
            BinaryNode d = Create(a, 0, a.Length - 1);
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
            Console.WriteLine(IsBst(d, null, null));

            return;

            static bool IsBst(BinaryNode node, int? min, int? max)
            {
                if (node is null)
                    return true;

                Console.WriteLine($"{min ?? int.MinValue} <= {node.Val} < {max ?? int.MaxValue}");
                if ((min != null && node.Val < min) || (max != null & node.Val > max))
                    return false;

                if (!IsBst(node.Left, min, node.Val) || !IsBst(node.Right, node.Val, max))
                    return false;

                return true;
            }
        }

        public static void IsBstNoDup_InOrder()
        {
            // int[] a = new int[] { 6, 7, 8, 10, 5, 12, 13 };
            int[] a = new int[] { 6, 7, 8, 10, 11, 12, 13 };
            BinaryNode d = Create(a, 0, a.Length - 1);
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
            Console.WriteLine(IsBst(d, null));

            return;

            static bool IsBst(BinaryNode node, BinaryNode lastVisited)
            {
                if (node != null)
                {
                    bool left = IsBst(node.Left, lastVisited);
                    if (!left)
                        return false;

                    if (lastVisited != null && node.Val < lastVisited.Val)
                        return false;

                    bool right = IsBst(node.Right, node);
                    if (!right)
                        return false;
                }
                return true;
            }

        }

        public static void CheckBalanced()
        {
            int[] a = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            BinaryNode d = Create(a, 0, a.Length - 1);
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

            Console.WriteLine(IsBalanced(d));

            return;

            static bool IsBalanced(BinaryNode node)
            {
                if (node is null)
                    return true;

                return Math.Abs(CalcHeight(node.Right) - CalcHeight(node.Left)) <= 1 &&
                       IsBalanced(node.Left) && IsBalanced(node.Right);
            }

            static int CalcHeight(BinaryNode node)
            {
                if (node is null)
                    return 0;

                return 1 + CalcHeight(node.Left) + CalcHeight(node.Right);
            }

        }

        public static void ListOfDepth()
        {
            int[] a = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            BinaryNode d = Create(a, 0, a.Length - 1);
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
            List<System.Collections.Generic.LinkedList<BinaryNode>> l = new List<System.Collections.Generic.LinkedList<BinaryNode>>();
            Load(d, l, 0);
            foreach (var node in l)
            {
                foreach (var n in node)
                {
                    Console.Write(n.Val);
                }
                Console.WriteLine();
            }

            return;

            static void Load(BinaryNode node, List<System.Collections.Generic.LinkedList<BinaryNode>> l, int level)
            {
                if (node is null)
                    return;

                if (l.Count < level + 1)
                    l.Add(new System.Collections.Generic.LinkedList<BinaryNode>());

                l[level].AddLast(node);

                Load(node.Left, l, level + 1);
                Load(node.Right, l, level + 1);
            }

        }

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
