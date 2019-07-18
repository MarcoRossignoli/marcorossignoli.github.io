using System;
using System.Linq;
using System.Text;

namespace Interview
{
    public static class RandomExercise
    {
        public static void ReverseStringInPlace()
        {
            string str = "this is a string";

            char[] strReversed = new char[str.Length];

            int l = 0;
            int r = str.Length - 1;

            while (l < r)
            {
                strReversed[r] = str[l];
                strReversed[l] = str[r];
                l++;
                r--;
            }

            Console.WriteLine(new string(strReversed));
        }

        public static void AddTwoNumbersRepresentedByLinkedLists()
        {
            // creating first list 
            LinkedList<int> list = new LinkedList<int>();
            list.Head = new Node<int> { Data = 7 };
            list.Head.Next = new Node<int> { Data = 5 };
            list.Head.Next.Next = new Node<int> { Data = 9 };
            list.Head.Next.Next.Next = new Node<int> { Data = 4 };
            list.Head.Next.Next.Next.Next = new Node<int> { Data = 6 };
            Console.Write("First List is ");
            printList(list.Head);

            // creating seconnd list 
            LinkedList<int> list1 = new LinkedList<int>();
            list1.Head = new Node<int> { Data = 8 };
            list1.Head.Next = new Node<int> { Data = 4 };
            Console.Write("First List1 is ");
            printList(list1.Head);

            int listLen = Count(list.Head);
            int list1Len = Count(list1.Head);

            Node<int> outermost = listLen > list1Len ? list.Head : list1.Head;
            int outermostCount = listLen > list1Len ? listLen : list1Len;
            Node<int> innermost = listLen <= list1Len ? list.Head : list1.Head;

            Node<int> head = null;

            int carry = 0;
            for (int i = outermostCount; i >= 1; i--)
            {
                int a = GetNValue(outermost, i);
                int b = i - Math.Abs(list1Len - listLen) < 1 ? 0 : GetNValue(innermost, i - Math.Abs(list1Len - listLen));
                int sum = a + b + carry;
                carry = sum >= 10 ? 1 : 0;
                sum = sum % 10;

                Node<int> ni = new Node<int>();
                ni.Data = sum;

                if (head is null)
                {
                    head = ni;
                }
                else
                {
                    ni.Next = head;
                    head = ni;
                }

            }

            Console.Write("Final is ");
            printList(head);

            return;

            static void printList(Node<int> head)
            {
                while (head != null)
                {
                    Console.Write(head.Data + " ");
                    head = head.Next;
                }
                Console.WriteLine("");
            }

            static int Count(Node<int> node)
            {
                if (node is null)
                {
                    return 0;
                }

                return Count(node.Next) + 1;
            }

            static int GetNValue(Node<int> node, int v)
            {
                if (v == 1)
                {
                    return node.Data;
                }

                return GetNValue(node.Next, v - 1);
            }
        }

        public static void AddTwoNumbersRepresentedByLinkedListsReversed()
        {
            // creating first list 
            LinkedList<int> list = new LinkedList<int>();
            list.Head = new Node<int> { Data = 7 };
            list.Head.Next = new Node<int> { Data = 5 };
            list.Head.Next.Next = new Node<int> { Data = 9 };
            list.Head.Next.Next.Next = new Node<int> { Data = 4 };
            list.Head.Next.Next.Next.Next = new Node<int> { Data = 6 };
            Console.Write("First List is ");
            printList(list.Head);

            // creating seconnd list 
            LinkedList<int> list1 = new LinkedList<int>();
            list1.Head = new Node<int> { Data = 8 };
            list1.Head.Next = new Node<int> { Data = 4 };
            Console.Write("Second List is ");
            printList(list1.Head);

            printList(Sum(list, list1).Head);

            return;

            static LinkedList<int> Sum(LinkedList<int> list1, LinkedList<int> list2)
            {
                LinkedList<int> res = new LinkedList<int>();
                Node<int> a = list1.Head;
                Node<int> b = list2.Head;
                int carry = 0;

                while (a != null || b != null)
                {
                    int sum = (a == null ? 0 : a.Data) + (b == null ? 0 : b.Data) + carry;

                    carry = (sum >= 10) ? 1 : 0;

                    sum = sum % 10;

                    res.Append(sum);

                    if (a != null)
                    {
                        a = a.Next;
                    }

                    if (b != null)
                    {
                        b = b.Next;
                    }

                }

                if (carry > 0)
                {
                    res.Append(carry);
                }


                return res;
            }

            static void printList(Node<int> head)
            {
                while (head != null)
                {
                    Console.Write(head.Data + " ");
                    head = head.Next;
                }
                Console.WriteLine("");
            }

            static int Count(Node<int> node)
            {
                if (node is null)
                {
                    return 0;
                }

                return Count(node.Next) + 1;
            }

            static int GetNValue(Node<int> node, int v)
            {
                if (v == 1)
                {
                    return node.Data;
                }

                return GetNValue(node.Next, v - 1);
            }
        }

        public static void CountChangeCoin()
        {
            int[] arr = { 1, 2, 3 };
            int m = arr.Length;
            Console.Write(getNumberOfWays(9, arr));

            return;

            static long getNumberOfWays(int N, int[] Coins)
            {
                // Create the ways array to 1 plus the amount  
                // to stop overflow  
                long[] ways = new long[(int)N + 1];

                // Set the first way to 1 because its 0 and  
                // there is 1 way to make 0 with 0 coins  
                ways[0] = 1;

                // Go through all of the coins  
                for (int i = 0; i < Coins.Length; i++)
                {

                    // Make a comparison to each index value  
                    // of ways with the coin value.  
                    for (int j = 0; j < ways.Length; j++)
                    {
                        if (Coins[i] <= j)
                        {

                            // Update the ways array  
                            ways[j] += ways[(int)(j - Coins[i])];
                        }
                    }
                }

                // return the value at the Nth position  
                // of the ways array.  
                return ways[(int)N];
            }
        }

        public static void SumMinLeaf()
        {
            TreeNode<int> node = new TreeNode<int>();
            node.Data = 1;
            node.Left = new TreeNode<int>();
            node.Left.Data = 2;
            node.Left.Left = new TreeNode<int>();
            node.Left.Left.Data = 4;
            node.Left.Right = new TreeNode<int>();
            node.Left.Right.Data = 5;
            node.Left.Left.Right = new TreeNode<int>();
            node.Left.Left.Right.Data = 2;
            node.Left.Left.Left = new TreeNode<int>();
            node.Left.Left.Left.Data = 7;
            node.Right = new TreeNode<int>();
            node.Data = 3;
            node.Right.Right = new TreeNode<int>();
            node.Right.Right.Data = 8;

            Console.WriteLine(SumMinLeaf(node));

            return;

            static int SumMinLeaf(TreeNode<int> node)
            {
                int sum = 0;
                bool end = false;
                System.Collections.Generic.Queue<TreeNode<int>> queue = new System.Collections.Generic.Queue<TreeNode<int>>();
                queue.Enqueue(node);

                while (!end)
                {
                    int levelElements = queue.Count;

                    while (levelElements-- > 0)
                    {
                        TreeNode<int> e = queue.Dequeue();

                        if (e.Left is null && e.Right is null)
                        {
                            sum += e.Data;
                            end = true;
                        }
                        else
                        {
                            if (e.Left != null)
                            {
                                queue.Enqueue(e.Left);
                            }
                            if (e.Right != null)
                            {
                                queue.Enqueue(e.Right);
                            }
                        }
                    }
                }

                return sum;
            }

        }

        public static void MaxSubarraySum()
        {
            int[] a = { -1, -2, -3, -4 };

            for (int i = 1; i < a.Length; i++)
            {
                if (a[i - 1] != 0)
                {
                    a[i] += a[i - 1];
                }
            }

            //int max = int.MinValue;

            //for (int i = 0; i < a.Length; i++)
            //{
            //    int subMax = a[i];
            //    max = Math.Max(subMax, max);

            //    for (int k = i + 1; k < a.Length; k++)
            //    {
            //        subMax += a[k];

            //        max = Math.Max(subMax, max);
            //    }
            //}

            //Console.WriteLine(max);
        }

        public static void PermutationDivisible()
        {
            int n = 31462708;
            // int n = 75;

            Console.WriteLine(Permutation(n.ToString(), ""));

            return;

            static bool Permutation(string str, string prefix)
            {
                if (str.Length == 0)
                {
                    return int.Parse(prefix) % 8 == 0;
                }

                for (int i = 0; i < str.Length; i++)
                {
                    string rem = str.Substring(i + 1) + str.Substring(0, i);
                    if (Permutation(rem, prefix + str[i]))
                    {
                        return true;
                    }
                }

                return false;
            }

        }

        public static void IdenticalTree()
        {
            TreeNode node4 = new TreeNode()
            {
                NodeVal = 4,
                Children = new TreeNode[0]
            };
            TreeNode node5 = new TreeNode()
            {
                NodeVal = 5,
                Children = new TreeNode[0]
            };
            TreeNode node2 = new TreeNode()
            {
                NodeVal = 2,
                Children = new TreeNode[] { node4 }
            };
            TreeNode node3 = new TreeNode()
            {
                NodeVal = 3,
                Children = new TreeNode[] { node5 }
            };
            TreeNode node1 = new TreeNode()
            {
                NodeVal = 1,
                Children = new TreeNode[] { node2, node3 }
            };

            Console.WriteLine(IsEqual(node1, node1));

            TreeNode node4B = new TreeNode()
            {
                NodeVal = 7,
                Children = new TreeNode[0]
            };
            TreeNode node5B = new TreeNode()
            {
                NodeVal = 5,
                Children = new TreeNode[0]
            };
            TreeNode node2B = new TreeNode()
            {
                NodeVal = 2,
                Children = new TreeNode[] { node4B }
            };
            TreeNode node3B = new TreeNode()
            {
                NodeVal = 3,
                Children = new TreeNode[] { node5B }
            };
            TreeNode node1B = new TreeNode()
            {
                NodeVal = 1,
                Children = new TreeNode[] { node2B, node3B }
            };

            Console.WriteLine(IsEqual(node1, node1B));

            return;

            static bool IsEqual(TreeNode node, TreeNode node2)
            {
                if (node.NodeVal != node2.NodeVal || node.Children.Length != node2.Children.Length)
                {
                    return false;
                }

                for (int i = 0; i < node.Children.Length; i++)
                {
                    return IsEqual(node.Children[i], node2.Children[i]);
                }

                return true;
            }

        }

        public static void Shortener()
        {
            string map = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string url;
            Console.WriteLine((url = GenerateName(1234678, map)));

            Console.WriteLine(GenerateInt(url, map).ToString());

            return;

            static string GenerateName(int number, string map)
            {
                StringBuilder stringBuilder = new StringBuilder();
                while (number != 0)
                {
                    int remainder = number % map.Length;
                    number /= map.Length;
                    stringBuilder.Append(map[remainder]);
                }

                return new string(stringBuilder.ToString().Reverse().ToArray());
            }

            static int GenerateInt(string url, string map)
            {
                int result = 0;


                for (int i = 0; i < url.Length; i++)
                {
                    for (int l = 0; l < map.Length; l++)
                    {
                        if (url[i] == map[l])
                        {
                            result += (int)Math.Pow(map.Length, url.Length - i - 1) * l;
                            break;
                        }
                    }
                }

                return result;
            }

        }


        public static void PrintPattern()
        {
            int n = 16;

            PrintPattern(n, n, true);

            n = 2;

            PrintPattern(n, n, true);

            n = 10;

            PrintPattern(n, n, true);

            PrintPatternBest(n);

            return;

            static void PrintPatternBest(int n)
            {

                // Base case (When n becomes 0 or
                // negative) 
                if (n == 0 || n < 0)
                {

                    Console.Write(n + " ");

                    return;
                }

                // First print decreasing order 
                Console.Write(n + " ");

                PrintPatternBest(n - 5);

                // Then print increasing order 
                Console.Write(n + " ");
            }

            static void PrintPattern(int start, int current, bool subtract)
            {
                if (current > start)
                {
                    Console.WriteLine("----");
                    return;
                }

                Console.WriteLine(current);

                if (current <= 0)
                {
                    PrintPattern(start, current + 5, false);
                }
                else
                {
                    PrintPattern(start, current + (subtract ? -5 : 5), subtract);
                }
            }
        }

        public static void TrappingRainWater()
        {
            int[] arr = new int[] { 3, 0, 0, 4, 0, 2 };

            int result = findWater(arr.Length, arr);

            static int findWater(int n, int[] arr)
            {
                // left[i] contains height of tallest bar to the 
                // left of i'th bar including itself 
                int[] left = new int[n];

                // Right [i] contains height of tallest bar to 
                // the right of ith bar including itself 
                int[] right = new int[n];

                // Initialize result 
                int water = 0;

                // Fill left array 
                left[0] = arr[0];
                for (int i = 1; i < n; i++)
                    left[i] = Math.Max(left[i - 1], arr[i]);

                // Fill right array 
                right[n - 1] = arr[n - 1];
                for (int i = n - 2; i >= 0; i--)
                    right[i] = Math.Max(right[i + 1], arr[i]);

                // Calculate the accumulated water element by element 
                // consider the amount of water on i'th bar, the 
                // amount of water accumulated on this particular 
                // bar will be equal to min(left[i], right[i]) - arr[i] . 
                for (int i = 0; i < n; i++)
                    water += Math.Min(left[i], right[i]) - arr[i];

                return water;
            }

        }

        public static void PrintMatrix()
        {
            int n = 3;
            int m = 3;
            int values = 0;
            int[,] matrix = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < m; k++)
                {
                    matrix[i, k] = values++;
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < m; k++)
                {
                    Console.Write(matrix[i, k]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            int[] currentPath = new int[n + m - 1];

            printMatrix(matrix, n, m, 0, 0, currentPath, 0);

            return;

            static void printMatrix(int[,] mat, int m, int n, int i, int j, int[] path, int idx)
            {
                path[idx] = mat[i, j];

                // Reached the bottom of the matrix so we are left with 
                // only option to move right 
                if (i == m - 1)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        path[idx + k - j] = mat[i, k];
                    }
                    for (int l = 0; l < idx + n - j; l++)
                    {
                        Console.Write(path[l] + " ");
                    }
                    Console.WriteLine();
                    return;
                }

                // Reached the right corner of the matrix we are left with 
                // only the downward movement. 
                if (j == n - 1)
                {
                    for (int k = i + 1; k < m; k++)
                    {
                        path[idx + k - i] = mat[k, j];
                    }
                    for (int l = 0; l < idx + m - i; l++)
                    {
                        Console.Write(path[l] + " ");
                    }
                    Console.WriteLine();
                    return;
                }

                // Print all the paths that are possible after moving down 
                printMatrix(mat, m, n, i + 1, j, path, idx + 1);

                // Print all the paths that are possible after moving right 
                printMatrix(mat, m, n, i, j + 1, path, idx + 1);
            }
        }

        public static void CountMatrixPath()
        {
            int n = 3;
            int m = 3;
            int values = 0;
            int[,] matrix = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < m; k++)
                {
                    matrix[i, k] = values++;
                }
            }

            Console.WriteLine(Count(matrix, n, m));

            return;

            static int Count(int[,] matrix, int n, int m)
            {

                if (n == 1 || m == 1)
                {
                    return 1;
                }

                return Count(matrix, n - 1, m) + Count(matrix, n, m - 1);
            }

        }
    }

    class TreeNode
    {
        public int NodeVal { get; set; }
        public TreeNode[] Children { get; set; }
    }
}
