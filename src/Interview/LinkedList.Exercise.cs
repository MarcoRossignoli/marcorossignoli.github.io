using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Interview.LinkedList.Excercise
{
    public class LinkedList
    {
        public static void LoopDetection_Pg225()
        {
            Node a = Node.Create(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
            a.ToList()[a.ToList().Length - 1].Next = a.ToList()[3];

            Console.WriteLine(LoopDetection(a) != null);
            Console.WriteLine(LoopDetection(a) != null ? LoopDetection(a).Val : -1);

            return;

            static Node LoopDetection(Node n)
            {
                Node slow = n;
                Node fast = n;

                while (fast != null && fast.Next != null)
                {
                    slow = slow.Next;
                    fast = fast.Next.Next;
                    if (slow == fast)
                    {
                        break;
                    }
                }

                if (fast is null || fast.Next is null)
                {
                    return null;
                }

                slow = n;

                while (slow != fast)
                {
                    slow = slow.Next;
                    fast = fast.Next;
                }

                return fast;
            }
        }

        public static void LoopDetection()
        {
            Node a = Node.Create(7, 8, 9, 0);
            a.ToList()[a.ToList().Length - 1].Next = a.ToList()[1];

            Console.WriteLine(LoopDetection(a) != null);
            Console.WriteLine(LoopDetection(a) != null ? LoopDetection(a).Val : -1);

            return;

            static Node LoopDetection(Node n)
            {
                if (n is null)
                    return n;

                List<Node> nodes = new List<Node>();
                while (n != null)
                {
                    if (nodes.Contains(n))
                    {
                        return n;
                    }
                    nodes.Add(n);
                    n = n.Next;
                }

                return null;
            }
        }

        public static void Intersect_Pg222()
        {
            Node tail = Node.Create(1, 2, 3);
            Node a = Node.Create(7, 8, 9, 0);
            Node b = Node.Create(4, 5);

            a.ToList()[a.ToList().Length - 1].Next = tail;
            b.ToList()[b.ToList().Length - 1].Next = tail;

            a.Print();
            b.Print();

            Console.WriteLine(Intersect(a, b) != null);
            Console.WriteLine(Intersect(a, b) != null ? Intersect(a, b).Val : -1);

            return;

            static Node Intersect(Node a, Node b)
            {
                var sta = GetSizeTail(a);
                var stb = GetSizeTail(b);

                if (sta.Item2 != stb.Item2)
                    return null;

                var shortOne = sta.Item1 < stb.Item1 ? a : b;
                var longOne = shortOne == a ? b : a;

                longOne = MoveLongTo(longOne, Math.Abs(sta.Item1 - stb.Item1));

                while (shortOne != longOne)
                {
                    shortOne = shortOne.Next;
                    longOne = longOne.Next;
                }

                return shortOne;
            }

            static Node MoveLongTo(Node n, int m)
            {
                while (m > 0 && n != null)
                {
                    n = n.Next;
                    m--;
                }
                return n;
            }

            static (int, Node) GetSizeTail(Node node)
            {
                if (node is null)
                    return (0, null);

                int size = 0;
                Node tail = null;
                while (node != null)
                {
                    tail = node;
                    size++;
                    node = node.Next;
                }

                return (size, tail);
            }
        }

        public static void Intersect_ButeForce()
        {
            Node tail = Node.Create(1, 2, 3);
            Node a = Node.Create(7, 8, 9, 0);
            Node b = Node.Create(4, 5);

            a.ToList()[a.ToList().Length - 1].Next = tail;
            b.ToList()[b.ToList().Length - 1].Next = tail;

            a.Print();
            b.Print();

            Console.WriteLine(Intersect(a, b) != null);
            Console.WriteLine(Intersect(a, b) != null ? Intersect(a, b).Val : -1);

            return;

            static Node Intersect(Node a, Node b)
            {
                for (Node n1 = a; n1 != null; n1 = n1.Next)
                {
                    for (Node n2 = b; n2 != null; n2 = n2.Next)
                    {
                        if (n1 == n2)
                        {
                            return n1;
                        }
                    }
                }
                return null;
            }
        }

        public static void Intersect()
        {
            Node tail = Node.Create(1, 2, 3);
            Node a = Node.Create(7, 8, 9, 0);
            Node b = Node.Create(4, 5);

            a.ToList()[a.ToList().Length - 1].Next = tail;
            b.ToList()[b.ToList().Length - 1].Next = tail;

            a.Print();
            b.Print();

            Console.WriteLine(Intersect(a, b) != null);
            Console.WriteLine(Intersect(a, b) != null ? Intersect(a, b).Val : -1);

            return;

            static Node Intersect(Node a, Node b)
            {
                var listA = Get(a);
                var listB = Get(b);

                Node intersectNode = null;

                int counterA = listA.Count - 1;
                int counterB = listB.Count - 1;

                while (counterA >= 0 && counterB >= 0)
                {
                    if (listA[counterA] == listB[counterB])
                    {
                        intersectNode = listA[counterA];
                    }
                    else
                    {
                        break;
                    }
                    counterA--;
                    counterB--;
                }


                return intersectNode;
            }

            static List<Node> Get(Node a)
            {
                List<Node> l = new List<Node>();
                while (a != null)
                {
                    l.Add(a);
                    a = a.Next;
                }
                return l;
            }

        }

        public static void IsPalindromeRunner()
        {
            // Node a = Node.Create(0, 1, 2, 3, 4, 3, 2, 1, 0);
            Node a = Node.Create(0, 1, 2, 3);

            Console.WriteLine(IsPalindrome(a));

            return;

            static bool IsPalindrome(Node a)
            {
                Stack<Node> s = new Stack<Node>();
                Node slow = a;
                Node fast = a;

                while (fast != null && fast.Next != null)
                {
                    s.Push(slow);
                    slow = slow.Next;
                    fast = fast.Next.Next;
                }

                if (fast != null)
                {
                    slow = slow.Next;
                }

                while (slow != null)
                {
                    if (s.Pop().Val != slow.Val)
                        return false;
                    slow = slow.Next;
                }

                return true;
            }
        }

        public static void IsPalindrome()
        {
            // Node a = Node.Create(1, 2, 1);
            // Node a = Node.Create(1, 2, 2, 1);
            // Node a = Node.Create(1, 2, 3);
            // Node a = Node.Create(1, 2, 3, 4, 5);
            // Node a = Node.Create(1, 1);
            // Node a = Node.Create(1);
            // Node a = Node.Create(1, 2, 2, 2, 1);
            // Node a = Node.Create(1, 2, 4, 1, 1);
            Node a = Node.Create(0, 1, 2, 3, 4, 3, 2, 1, 0);

            IsPalindrome(a, a, out bool ok);

            Console.WriteLine(ok);

            return;

            static Node IsPalindrome(Node n, Node h, out bool ok)
            {
                if (n is null)
                {
                    ok = true;
                    return null;
                }

                Node n1 = IsPalindrome(n.Next, h, out bool oki);

                if (!oki)
                {
                    ok = oki;
                    return null;
                }

                Node n2 = n1 is null ? h : n1;

                if (n2.Val != n.Val)
                {
                    ok = false;
                    return null;
                }
                else
                {
                    ok = true;
                    return n2.Next;
                }
            }

        }

        public static void SumListRecoursive()
        {
            Node a = Node.Create(9, 9, 9);
            Node b = Node.Create(9, 9, 9);

            Sum(a, b).Print();

            return;

            static Node Sum(Node a, Node b)
            {
                Node n = SumInt(a, b, out bool carry);
                if (carry)
                {
                    Node c = new Node(1);
                    c.Next = n;
                    return c;
                }
                else
                {
                    return n;
                }
            }

            static Node SumInt(Node a, Node b, out bool carry)
            {
                if (a is null && b is null)
                {
                    carry = false;
                    return null;
                }

                Node r = SumInt(a.Next, b.Next, out bool carryi);
                int sum = a.Val + b.Val + (carryi ? 1 : 0);
                carry = sum >= 10;
                Node c = new Node(carry ? sum % 10 : sum);

                if (r is null)
                {
                    return c;
                }
                else
                {
                    c.Next = r;
                    return c;
                }
            }
        }

        public static void SumLists()
        {
            Node a = Node.Create(7, 1, 6);
            // Node b = Node.Create(5, 9, 2);
            // Node b = Node.Create(5, 9);
            Node b = Node.Create(5, 9, 6);

            Sum(a, b).Print();

            return;

            static Node Sum(Node a, Node b)
            {
                int carry = 0;
                Node total = null;
                Node last = null;

                while (a != null || b != null)
                {
                    int valA = a == null ? 0 : a.Val;
                    int valB = b == null ? 0 : b.Val;
                    int sum = valA + valB + carry;
                    carry = sum >= 10 ? 1 : 0;
                    int nodeVal = carry > 0 ? (sum % 10) : sum;
                    if (total is null)
                    {
                        total = new Node(nodeVal);
                        last = total;
                    }
                    else
                    {
                        last.Next = new Node(nodeVal);
                        last = last.Next;
                    }
                    a = a?.Next;
                    b = b?.Next;
                }

                if (carry > 0)
                {
                    last.Next = new Node(1);
                }

                return total;
            }

        }

        public static void Partition_Pg212()
        {
            var nodes = Node.Create(new int[] { 5, 6, 7, 10, 2, 4 });
            // var nodes = Node.Create(new int[] { 5 });
            int x = 8;
            nodes.Print();

            Partition(nodes, x).Print();

            return;

            static Node Partition(Node node, int x)
            {
                Node head = node;
                Node tail = node;

                while (node != null)
                {
                    Node next = node.Next;
                    if (node.Val < x)
                    {
                        node.Next = head;
                        head = node;
                    }
                    else
                    {
                        tail.Next = node;
                        tail = tail.Next;
                    }
                    node = next;
                }
                tail.Next = null;
                return head;
            }
        }

        public static void RemoveMiddle_Pg211()
        {
            // var nodes = Node.Create(new int[] { 1, 2, 3 });
            var nodes = Node.Create(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            nodes.Print();

            var node = nodes.ToList()[2];

            Remove(node);

            nodes.Print();

            return;


            static void Remove(Node n)
            {
                if (n is null || n.Next is null)
                    throw new InvalidOperationException();

                n.Val = n.Next.Val;
                n.Next = n.Next.Next;
            }

        }

        public static void RemoveMiddle()
        {
            // var nodes = Node.Create(new int[] { 1, 2, 3 });
            var nodes = Node.Create(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            nodes.Print();

            var node = nodes.ToList()[2];

            Remove(node);

            nodes.Print();

            return;

            static void Remove(Node n)
            {
                while (n != null)
                {
                    int tmp = n.Val;
                    n.Val = n.Next.Val;
                    n.Next.Val = tmp;
                    if (n.Next.Next is null)
                    {
                        n.Next = null;
                        break;
                    }
                    n = n.Next;
                }
            }

        }

        public static void RemoveMiddleFromHead()
        {
            // var nodes = Node.Create(new int[] { 1, 2, 3 });
            // var nodes = Node.Create(new int[] { 1, 2 });
            // var nodes = Node.Create(new int[] { 1, 2, 3, 4, 5 });
            // var nodes = Node.Create(new int[] { 1, 2, 3, 4, 5, 6, 7 });
            // var nodes = Node.Create(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var nodes = Node.Create(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            nodes.Print();

            Remove(nodes);

            nodes.Print();

            return;

            static void Remove(Node head)
            {
                Node c = head;
                Node r = head;

                int m = 0;
                while (r.Next != null)
                {
                    if (m != 0 && m % 3 == 0)
                    {
                        c = c.Next;
                    }
                    else
                    {
                        r = r.Next;
                    }
                    m++;
                }

                if (c.Next != r)
                {
                    c.Next = c.Next.Next;
                }
            }
        }

        public static void KthToLast_Runner()
        {
            var nodes = Node.Create(new int[] { 1, 2, 3, 4, 5, 6 });
            int kth = 5;
            Node n = KthToLast(nodes, ref kth);
            Console.WriteLine(n != null ? n.Val.ToString() : "null");

            return;

            static Node KthToLast(Node n, ref int kth)
            {
                if (n is null)
                    return null;

                Node c = n;
                Node r = c;
                int m = kth;

                while (r.Next != null)
                {
                    if (m <= 0)
                    {
                        c = c.Next;
                    }
                    r = r.Next;
                    m--;
                }

                return m <= 0 ? c : null;
            }
        }

        public static void KthToLast()
        {
            var nodes = Node.Create(new int[] { 1, 2, 3, 4, 5, 6 });
            int kth = 5;
            Node n = KthToLast(nodes, ref kth);
            Console.WriteLine(n != null ? n.Val.ToString() : "null");

            return;

            static Node KthToLast(Node n, ref int kth)
            {
                if (n == null)
                    return null;

                Node n2 = KthToLast(n.Next, ref kth);

                if (n2 != null)
                    return n2;

                if (--kth == 0)
                    return n;
                else
                    return null;
            }
        }

        public static void RemoveDup_NoBuffer_Pg208_Runner()
        {
            var nodes = Node.Create(new int[] { 1, 1, 1, 1, 2, 2, 3, 3, 4, 5, 5, 5, 6 });
            nodes.Print();

            RemoveDup(nodes);

            nodes.Print();

            return;

            static void RemoveDup(Node head)
            {
                Node current = head;
                int i = 0;
                while (current != null)
                {
                    i++;
                    Node runner = current;
                    while (runner.Next != null)
                    {
                        i++;
                        if (runner.Next.Val == current.Val)
                        {
                            runner.Next = runner.Next.Next;
                        }
                        else
                        {
                            runner = runner.Next;
                        }
                    }
                    current = current.Next;
                }
                Console.WriteLine(i);
            }
        }

        public static void RemoveDup_ON_Pg208()
        {
            var nodes = Node.Create(new int[] { 1, 1, 1, 1, 2, 2, 3, 3, 4, 5, 5, 5, 6 });
            nodes.Print();

            RemoveDup(nodes);

            nodes.Print();

            return;

            static void RemoveDup(Node n)
            {
                HashSet<int> buffer = new HashSet<int>();
                Node prev = null;
                int i = 0;
                while (n != null)
                {
                    i++;
                    if (buffer.Contains(n.Val))
                    {
                        prev.Next = n.Next;
                    }
                    else
                    {
                        buffer.Add(n.Val);
                        prev = n;
                    }
                    n = n.Next;
                }
                Console.WriteLine(i);
            }
        }

        public static void RemoveDups_NoBuffer()
        {
            var nodes = Node.Create(new int[] { 1, 1, 1, 1, 2, 2, 3, 3, 4, 5, 5, 5, 6 });
            nodes.Print();

            RemoveDup(nodes);

            nodes.Print();

            return;

            static void RemoveDup(Node head)
            {
                if (head is null)
                    return;

                Node n = head;
                int l = 0;

                int i = 0;
                while (n.Next != null)
                {
                    i++;
                    Node inner = head;
                    bool found = false;
                    int lt = l;

                    while (lt >= 0)
                    {
                        i++;
                        if (inner.Val == n.Next.Val)
                        {
                            n.Next = n.Next.Next;
                            found = true;
                            break;
                        }
                        inner = inner.Next;
                        lt--;
                    }

                    if (!found)
                    {
                        l++;
                        n = n.Next;
                    }

                }
                Console.WriteLine(i);
            }
        }

        public static void RemoveDups_WithBuffer()
        {
            var nodes = Node.Create(new int[] { 1, 1, 1, 1, 2, 2, 3, 3, 4, 5, 5, 5, 6 });
            nodes.Print();

            RemoveDup(nodes);

            nodes.Print();

            return;

            static void RemoveDup(Node head)
            {
                if (head is null)
                    return;

                HashSet<int> buffer = new HashSet<int>
                {
                    head.Val
                };

                Node n = head;
                int i = 0;
                while (n.Next != null)
                {
                    i++;
                    if (buffer.Contains(n.Next.Val))
                    {
                        n.Next = n.Next.Next;
                    }
                    else
                    {
                        buffer.Add(n.Next.Val);
                        n = n.Next;
                    }
                }

                Console.WriteLine(i);
            }
        }


        [DebuggerDisplay("{Val}")]
        class Node
        {
            public Node(int val)
            {
                Val = val;
            }

            public int Val { get; set; }
            public Node Next { get; set; }

            public static Node Create(params int[] vals)
            {
                Node head = new Node(vals[0]);
                Node last = head;

                for (int i = 1; i < vals.Length; i++)
                {
                    Node n = new Node(vals[i]);
                    last.Next = n;
                    last = last.Next;
                }

                return head;
            }

            public Node[] ToList()
            {
                List<Node> node = new List<Node>();
                Node n = this;
                while (n != null)
                {
                    node.Add(n);
                    n = n.Next;
                }
                return node.ToArray();
            }

            public void Print()
            {
                Node n = this;

                do
                {
                    Console.WriteLine(n.Val);
                    n = n.Next;
                }
                while (n != null);

                Console.WriteLine();
            }
        }
    }
}
