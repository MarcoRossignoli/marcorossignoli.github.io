using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using fxLinkedListInt = System.Collections.Generic.LinkedList<int>;
namespace Interview
{
    class TreesAndGraphs
    {
        public static void PopulatingNextRightPoint()
        {
            Node one = new Node() { val = 1 };
            Node two = new Node() { val = 2 };
            Node three = new Node() { val = 3 };
            Node four = new Node() { val = 4 };
            Node five = new Node() { val = 5 };
            Node six = new Node() { val = 6 };
            Node seven = new Node() { val = 7 };
            Node eight = new Node() { val = 8 };

            one.left = two;
            one.right = three;

            two.left = four;
            two.right = five;

            four.left = seven;

            three.right = six;
            six.right = eight;

            ConnectHelper(one);


            static void ConnectHelper(Node root)
            {
                if (root == null)
                {
                    return;
                }

                Node temp = root;
                Node start = null;
                Node prev = new Node(-1, null, null, null);

                while (temp != null)
                {
                    if (start == null)
                    {
                        start = temp.left ?? temp.right;
                    }

                    if (temp.left != null)
                    {
                        prev.next = temp.left;
                        prev = temp.left;
                    }

                    if (temp.right != null)
                    {
                        prev.next = temp.right;
                        prev = temp.right;
                    }

                    temp = temp.next;
                }

                ConnectHelper(start);
            }
        }

        [DebuggerDisplay("{val}")]
        class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node next;

            public Node() { }
            public Node(int _val, Node _left, Node _right, Node _next)
            {
                val = _val;
                left = _left;
                right = _right;
                next = _next;
            }
        }

        public static void InOrderTraversalIterative()
        {
            TreeNode root = new TreeNode(1);
            root.left = new TreeNode(2);
            root.right = new TreeNode(3);

            HashSet<int> l = new HashSet<int>();
            Stack<TreeNode> s = new Stack<TreeNode>();

            s.Push(root);

            while (!s.IsEmpty())
            {
                var f = s.Peek();

                if (f.left != null)
                {
                    if (!l.Contains(f.left.val))
                    {
                        s.Push(f.left);
                        continue;
                    }
                }

                if (
                    (
                        f.left == null || l.Contains(f.left.val)) &&
                        f.right != null
                    )
                {
                    l.Add(s.Pop().val);

                    if (!l.Contains(f.right.val))
                    {
                        s.Push(f.right);
                        continue;
                    }
                }

                l.Add(s.Pop().val);
            }
        }

        class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

        public static void PathWithSum()
        {
            BinarySearchTreeNode root = new BinarySearchTreeNode(10);
            root.Insert(5);
            root.Insert(20);
            root.Insert(-2);
            root.Insert(6);
            root.Insert(-17);
            root.Insert(21);

            int numberOfSum = NumberOfSum(root, 21);

            Console.WriteLine(numberOfSum);

            return;

            int NumberOfSum(BinarySearchTreeNode node, int val)
            {
                if (node != null)
                {
                    // sum pre-order top down
                    return
                        SumFromNode(node, 0, val) +

                        NumberOfSum(node.Left, val) +
                        NumberOfSum(node.Right, val);
                }
                return 0;
            }

            int SumFromNode(BinarySearchTreeNode node, int sum, int targetSum)
            {
                if (node is null)
                    return 0;

                int currentSumPreOrder = sum + node.Val;
                if (currentSumPreOrder == targetSum)
                    return 1;

                int leftSum = SumFromNode(node.Left, currentSumPreOrder, targetSum);
                int rightSum = SumFromNode(node.Right, currentSumPreOrder, targetSum);

                return leftSum + rightSum;
            }

        }

        public static void BinaryTreeInsertDelete()
        {
            BinarySearchTreeNode root = new BinarySearchTreeNode(5);
            root.Insert(3);
            root.Insert(7);
            root.Insert(1);
            root.Insert(4);
            root.Insert(6);
            root.Insert(10);

            var node = root.Find(10);
            Console.WriteLine(root.Verify());

            root = root.Remove(7);
            Console.WriteLine(root.Verify());

            root = root.Remove(5);
            Console.WriteLine(root.Verify());
        }

        [DebuggerDisplay("{Val}")]
        class BinarySearchTreeNode
        {
            public BinarySearchTreeNode(int val)
            {
                Val = val;
            }
            public int Val { get; set; }
            public BinarySearchTreeNode Left { get; set; }
            public BinarySearchTreeNode Right { get; set; }

            public bool Verify()
            {
                return Verify(this);
            }

            private bool Verify(BinarySearchTreeNode node)
            {
                if (node is null)
                    return true;


                if ((node.Left is null ? int.MinValue : node.Left.Val) <= node.Val &&
                        (node.Right is null ? int.MaxValue : node.Right.Val) > node.Val)
                    return Verify(node.Left) && Verify(node.Right);
                else
                    return false;
            }

            public void Insert(int val)
            {
                if (val <= Val)
                {
                    // Go left
                    if (Left is null)
                        Left = new BinarySearchTreeNode(val);
                    else
                        Left.Insert(val);
                }
                else
                {
                    // Go right
                    if (Right is null)
                        Right = new BinarySearchTreeNode(val);
                    else
                        Right.Insert(val);
                }
            }

            // https://www.geeksforgeeks.org/binary-search-tree-set-2-delete/
            public BinarySearchTreeNode Remove(int val)
            {
                return RemoveInternal(this, val);
            }

            private BinarySearchTreeNode RemoveInternal(BinarySearchTreeNode node, int val)
            {
                if (node is null)
                    return null;

                // Search node
                if (node.Val > val)
                {
                    node.Left = RemoveInternal(node.Left, val);
                }
                else if (node.Val < val)
                {
                    node.Right = RemoveInternal(node.Right, val);
                }
                else
                {
                    // node found

                    // if left or right is null return other node
                    // this cover 2 case 
                    // 1) leaf node
                    // 2) node with only one child
                    if (node.Left is null)
                        return node.Right;

                    if (node.Right is null)
                        return node.Left;

                    // node has two child case number 3
                    // find next in order and swap
                    int min = GetNextInOrder(node.Right);
                    node.Val = min;

                    // remove the min
                    node.Right = RemoveInternal(node.Right, min);
                }

                return node;
            }

            private int GetNextInOrder(BinarySearchTreeNode node)
            {
                Debug.Assert(node != null);

                int min = 0;

                while (node != null)
                {
                    min = node.Val;
                    node = node.Left;
                }

                return min;
            }

            public BinarySearchTreeNode Find(int val)
            {
                if (val == Val)
                    return this;

                if (val < Val)
                {
                    if (Left is null)
                        return null;

                    return Left.Find(val);
                }
                else
                {
                    if (Right is null)
                        return null;

                    return Right.Find(val);
                }
            }
        }

        public static void RandomNodeStart()
        {
            RandomBinaryTree bt = new RandomBinaryTree();
            bt.Insert(1);
            bt.Insert(2);
            bt.Insert(3);

            var binaryNodes = bt.Find(1);
            var randomNode = bt.GetRandom();

            bt.Remove(binaryNodes[0]);
        }

        class RandomBinaryTree
        {
            internal RandomNode[] _array;
            internal int _size;

            public RandomBinaryTree()
            {
                _array = new RandomNode[10];
            }

            void EnsureCapacity(int size)
            {
                if (_array.Length < size)
                {
                    RandomNode[] tmp = new RandomNode[_array.Length * 2];
                    _array.CopyTo(tmp, 0);
                    _array = tmp;
                }
            }

            public RandomNode[] Find(int v)
            {
                List<RandomNode> randomNode = new List<RandomNode>();
                for (int i = 0; i < _size; i++)
                {
                    if (_array[i].Val == v)
                        randomNode.Add(_array[i]);
                }
                return randomNode.ToArray();
            }

            public RandomNode GetRandom()
            {
                if (_size > 0)
                {
                    int randomIndex = Environment.TickCount % _size;
                    return _array[randomIndex];
                }
                return null;
            }

            public void Remove(RandomNode node)
            {
                if (node._detached)
                    throw new Exception("detached");


                if (_size > 1)
                {
                    _array[node._index] = _array[_size - 1];
                    _array[node._index]._index = node._index;
                    _array[_size - 1] = null;
                }

                node.Detach();
                _size--;
            }

            public void Insert(int i)
            {
                EnsureCapacity(_size + 1);
                _array[_size] = new RandomNode(_size, this, i);
                _size++;
            }

        }

        class RandomNode
        {
            public RandomNode Left
            {
                get
                {
                    if (_detached)
                        throw new Exception("detached");

                    int leftIndex = (_index * 2) + 1;
                    if (leftIndex > _binaryTree._size - 1)
                        return null;

                    return _binaryTree._array[leftIndex];
                }
            }

            public RandomNode Right
            {
                get
                {
                    if (_detached)
                        throw new Exception("detached");

                    int rightIndex = (_index * 2) + 2;
                    if (rightIndex > _binaryTree._size - 1)
                        return null;

                    return _binaryTree._array[rightIndex];
                }
            }

            internal bool _detached = true;
            RandomBinaryTree _binaryTree;
            internal int _index { get; set; }
            public int Val { get; set; }
            public RandomNode(int index, RandomBinaryTree binaryTree, int val)
            {
                _index = index;
                _binaryTree = binaryTree;
                Val = val;
                _detached = false;
            }

            internal void Detach()
            {
                _binaryTree = null;
                _detached = true;
            }
        }

        public static void CheckSubtree()
        {
            /*
                1 <- a
               / \
         b <- 2   3
             / \
            4   5
           / \
          6   7
            */

            BinaryNode a = new BinaryNode(1);
            a.Left = new BinaryNode(2);
            a.Right = new BinaryNode(3);
            a.Left.Left = new BinaryNode(4);
            a.Left.Right = new BinaryNode(5);
            a.Left.Left.Left = new BinaryNode(6);
            a.Left.Left.Right = new BinaryNode(7);

            // BinaryNode b = a.Left;
            BinaryNode b = new BinaryNode(2);
            b.Left = new BinaryNode(4);
            b.Right = new BinaryNode(5);

            int heightB = Height(b);
            Console.WriteLine(Check(a, b, heightB));

            return;

            static (int currentHeight, bool foundSubTree) Check(BinaryNode a, BinaryNode b, int height)
            {
                if (a is null)
                    return (-1, false);

                (int currentHeightLeft, bool foundSubTreeLeft) = Check(a.Left, b, height);

                if (foundSubTreeLeft)
                    return (0, true);

                if (currentHeightLeft == height && EqualSubtree(a.Left, b))
                {
                    return (0, true);
                }

                (int currentHeightRight, bool foundSubTreeRight) = Check(a.Right, b, height);

                if (foundSubTreeRight)
                    return (0, true);

                if (currentHeightRight == height && EqualSubtree(a.Right, b))
                {
                    return (0, true);
                }

                return (Math.Max(currentHeightLeft, currentHeightRight) + 1, false);
            }


            static bool EqualSubtree(BinaryNode first, BinaryNode second)
            {
                if (first is null && second is null)
                    return true;

                if (first is null && second != null || first != null && second is null)
                    return false;

                if (first.Val != second.Val)
                    return false;

                bool l = EqualSubtree(first.Left, second.Left);
                bool r = EqualSubtree(first.Right, second.Right);

                if (!(l && r))
                    return false;

                return true;
            }

            static int Height(BinaryNode node)
            {
                if (node is null)
                    return -1;

                return Math.Max(Height(node.Left), Height(node.Right)) + 1;
            }

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
