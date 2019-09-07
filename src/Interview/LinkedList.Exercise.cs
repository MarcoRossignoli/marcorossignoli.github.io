using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Interview.LinkedList.Excercise
{
    public class LinkedList
    {
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

            public static Node Create(int[] vals)
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
