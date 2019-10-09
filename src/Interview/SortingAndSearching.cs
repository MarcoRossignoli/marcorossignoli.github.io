using System;
using System.Diagnostics;
using System.IO;

namespace Interview
{
    // 157
    // 407
    class SortingAndSearching
    {
        public static void PeakValleySort()
        {
            int[] a = new int[] { 5, 3, 1, 2, 3 };

            QuickSort(a, 0, a.Length - 1);

            for (int i = 1; i < a.Length; i += 2)
            {
                Swap(a, i - 1, i);
            }

            //int[] t = new int[a.Length];
            //Array.Copy(a, t, a.Length);

            //int s = 0;
            //int e = a.Length - 1;
            //int i = 0;

            //while (s <= e)
            //{
            //    if (s < e)
            //    {
            //        a[i++] = t[e];
            //        a[i++] = t[s];
            //    }
            //    else
            //    {
            //        a[i] = t[e];
            //    }
            //    s++;
            //    e--;
            //}

            foreach (var item in a)
            {
                Console.WriteLine(item);
            }

            return;

            static void Swap(int[] array, int a, int b)
            {
                int tmp = array[a];
                array[a] = array[b];
                array[b] = tmp;
            }

            static void QuickSort(int[] a, int l, int r)
            {
                int partitionIndex = Partition(a, l, r);

                if (l < partitionIndex - 1)
                {
                    QuickSort(a, l, partitionIndex - 1);
                }

                if (partitionIndex < r)
                {
                    QuickSort(a, partitionIndex, r);
                }
            }

            static int Partition(int[] a, int l, int r)
            {
                int pivot = a[(l + r) / 2];
                int lt = l;
                int rt = r;
                while (lt <= rt)
                {
                    while (a[lt] < pivot)
                        lt++;
                    while (a[rt] > pivot)
                        rt--;
                    if (lt <= rt)
                    {
                        int tmp = a[lt];
                        a[lt] = a[rt];
                        a[rt] = tmp;
                        lt++;
                        rt--;
                    }
                }
                return lt;
            }

        }

        public static void RankFromStream()
        {
            RankNode rn = new RankNode(5);
            rn.Insert(1);
            rn.Insert(4);
            rn.Insert(4);
            rn.Insert(5);
            rn.Insert(9);
            rn.Insert(7);
            rn.Insert(13);
            rn.Insert(3);

            Console.WriteLine(rn.GetRank(3));
        }

        [DebuggerDisplay("{_data}")]
        class RankNode
        {
            int _data;

            RankNode _left;
            RankNode _right;

            int _countLeft = 0;
            public RankNode(int data)
            {
                _data = data;
            }

            public void Insert(int data)
            {
                if (data <= _data)
                {
                    if (_left is null)
                        _left = new RankNode(data);
                    else
                        _left.Insert(data);
                    _countLeft++;
                }
                else
                {
                    if (_right is null)
                        _right = new RankNode(data);
                    else
                        _right.Insert(data);
                }
            }

            public int GetRank(int data)
            {
                if (_data == data)
                    return _countLeft;
                else if (data <= _data)
                {
                    if (_left is null)
                        return -1;
                    else
                        return _left.GetRank(data);
                }
                else
                {
                    int countRight = _right is null ? -1 : _right.GetRank(data);
                    if (countRight == -1)
                        return -1;
                    else
                        return _countLeft + 1 + countRight;
                }
            }
        }

        public static void SortedMatrix()
        {
            int[][] m = new int[][]
            {
                new int[] {15,20,40,85},
                new int[] {20,35,80,95},
                new int[] {30,55,95,105},
                new int[] {40,80,100,120},
            };

            Console.WriteLine("(r,c) " + Find(m, 55));

            return;

            static (int r, int c) Find(int[][] m, int v)
            {
                if (m.Length == 0)
                    return (-1, -1);

                for (int r = 0, c = 0; r < m.Length && c < m[0].Length; r++, c++)
                {
                    if (m[r][c] == v)
                        return (r, c);

                    if (v < m[r][c])
                        return (-1, -1);

                    (int rc, int cc) = SearchCol(m, r, c + 1, m[0].Length - 1, v);
                    if (rc != -1)
                        return (rc, cc);

                    (int rr, int cr) = SearchRow(m, c, r + 1, m.Length - 1, v);
                    if (rr != -1)
                        return (rr, cr);
                }

                return (-1, -1);
            }

            static (int r, int c) SearchRow(int[][] m, int c, int s, int e, int v)
            {
                while (s <= e)
                {
                    int mid = (s + e) / 2;
                    if (m[mid][c] == v)
                        return (mid, c);
                    if (m[mid][c] > v)
                        e = mid - 1;
                    else
                        s = mid + 1;
                }
                return (-1, -1);
            }

            static (int r, int c) SearchCol(int[][] m, int r, int s, int e, int v)
            {
                while (s <= e)
                {
                    int mid = (s + e) / 2;
                    if (m[r][mid] == v)
                        return (r, mid);
                    if (m[r][mid] > v)
                        e = mid - 1;
                    else
                        s = mid + 1;
                }
                return (-1, -1);
            }
        }

        public static void FindDup()
        {
            int[] a = new int[] {
                                    0, 1, 2, 3, 4, 5, 6, 7,
                                    8, 9, 10, 10 , 0 , 7 ,7,
                                    11, 12, 34, 55, 33, 66, 66
                                };

            byte[] m = new byte[1024 * 4];
            System.Collections.Generic.HashSet<int> dup = new System.Collections.Generic.HashSet<int>();
            for (int i = 0; i < a.Length; i++)
            {
                int arrayIndex = a[i] / 8;
                int bitIndex = a[i] % 8;

                if ((m[arrayIndex] & (1 << bitIndex)) == (1 << bitIndex))
                {
                    dup.Add(a[i]);
                }
                else
                {
                    m[arrayIndex] |= (byte)(1 << bitIndex);
                }
            }

            foreach (var item in dup)
            {
                Console.WriteLine(item);
            }

        }

        public static void QuickSortAndMergeSorteIterative()
        {
            int[] a = new int[] { 4, 3, 5, 6, 8, 345, 7, 5, 2 };

            QuickSortIterative(a, 0, a.Length - 1);

            foreach (var v in a)
            {
                Console.WriteLine(v);
            }

            Console.WriteLine();

            a = new int[] { 4, 3, 5, 6, 8, 345, 7, 5, 2 };

            MergeSortIterative(a, 0, a.Length - 1);

            foreach (var v in a)
            {
                Console.WriteLine(v);
            }

            return;

            static void MergeSortIterative(int[] a, int l, int h)
            {
                int curr_size;
                int left_start;
                for (curr_size = 1; curr_size <= a.Length - 1; curr_size = 2 * curr_size)
                {
                    for (left_start = 0; left_start < a.Length - 1; left_start += 2 * curr_size)
                    {
                        int mid = left_start + curr_size - 1;
                        int right_end = Math.Min(left_start + 2 * curr_size - 1, a.Length - 1);
                        Merge(a, left_start, mid, right_end);
                    }
                }
            }

            static void Merge(int[] arr, int l, int m, int r)
            {
                int i, j, k;
                int n1 = m - l + 1;
                int n2 = r - m;

                int[] L = new int[n1];
                int[] R = new int[n2];

                for (i = 0; i < n1; i++)
                    L[i] = arr[l + i];
                for (j = 0; j < n2; j++)
                    R[j] = arr[m + 1 + j];

                i = 0;
                j = 0;
                k = l;
                while (i < n1 && j < n2)
                {
                    if (L[i] <= R[j])
                    {
                        arr[k] = L[i];
                        i++;
                    }
                    else
                    {
                        arr[k] = R[j];
                        j++;
                    }
                    k++;
                }

                while (i < n1)
                {
                    arr[k] = L[i];
                    i++;
                    k++;
                }
                while (j < n2)
                {
                    arr[k] = R[j];
                    j++;
                    k++;
                }
            }

            static void QuickSortIterative(int[] a, int l, int h)
            {
                Stack<int> stack = new Stack<int>();

                stack.Push(l);
                stack.Push(h);

                while (!stack.IsEmpty())
                {
                    int ch = stack.Pop();
                    int cl = stack.Pop();

                    int i = Partition(a, cl, ch);

                    if (cl < i - 1)
                    {
                        stack.Push(cl);
                        stack.Push(i - 1);
                    }

                    if (i < ch)
                    {
                        stack.Push(i);
                        stack.Push(ch);
                    }

                }
            }

            static int Partition(int[] array, int left, int right)
            {
                int pivot = (left + right) / 2;

                while (left <= right)
                {
                    while (array[left] < array[pivot])
                        left++;

                    while (array[right] > array[pivot])
                        right--;

                    if (left <= right)
                    {
                        Swap(array, left, right);
                        left++;
                        right--;
                    }
                }

                return left;
            }

            static void Swap(int[] array, int a, int b)
            {
                int tmp = array[a];
                array[a] = array[b];
                array[b] = tmp;
            }
        }

        public static void SparseSearch_Pg401()
        {
            string[] a = new string[] { "at", "", "", "", "ball", "", "", "car", "", "", "dad", "", "" };

            Console.WriteLine(S(a, 0, a.Length - 1, "ball"));

            return;

            static int S(string[] a, int l, int r, string s)
            {
                while (l <= r)
                {
                    int mid = (l + r) / 2;

                    if (a[mid] == "")
                    {
                        int li = mid - 1;
                        int ri = mid + 1;

                        while (true)
                        {

                            if (li < l && ri > r)
                                return -1;
                            else if (ri <= r && a[ri] != "")
                            {
                                mid = ri;
                                break;
                            }
                            else if (li >= l && a[li] != "")
                            {
                                mid = li;
                                break;
                            }
                            li--;
                            ri++;
                        }
                    }

                    if (a[mid].CompareTo(s) == 0)
                        return mid;
                    else if (a[mid].CompareTo(s) == -1)
                        return S(a, mid + 1, r, s);
                    else
                        return S(a, l, mid - 1, s);
                }
                return -1;
            }

        }

        public static void SparseSearch()
        {
            string[] a = new string[] { "at", "", "", "", "ball", "", "", "car", "", "", "dad", "", "" };

            Console.WriteLine(S(a, 0, a.Length - 1, "ball"));

            return;

            static int S(string[] a, int l, int r, string s)
            {
                while (l <= r)
                {
                    int mid = (l + r) / 2;

                    if (a[mid] == s)
                        return mid;

                    if (a[mid] == "")
                    {
                        int ls = S(a, l, mid - 1, s);
                        if (ls != -1)
                            return ls;
                        int rs = S(a, mid + 1, r, s);
                        if (rs != -1)
                            return rs;
                    }

                    if (a[mid].CompareTo(s) == -1)
                        return S(a, mid + 1, r, s);
                    else
                        return S(a, l, mid - 1, s);
                }
                return -1;
            }

        }

        public static void SortedSearchNoSize_Pg411()
        {
            ArrayNoSize a = new ArrayNoSize(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            Console.WriteLine(Find(a, 9));

            static int Find(ArrayNoSize a, int x)
            {
                int index = 1;
                while (a.ItemAt(index) > -1 && a.ItemAt(index) < x)
                {
                    index *= 2;
                }

                return Search(a, index / 2, index, x);
            }

            static int Search(ArrayNoSize a, int l, int r, int x)
            {
                while (l <= r)
                {
                    int mid = (l + r) / 2;

                    if (a.ItemAt(mid) == x)
                        return mid;

                    if (a.ItemAt(mid) == -1 || a.ItemAt(mid) > x)
                    {
                        r = mid - 1;
                    }
                    else
                    {
                        l = mid + 1;
                    }
                }

                return -1;
            }
        }

        public static void SortedSearchNoSize()
        {
            ArrayNoSize a = new ArrayNoSize(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            Console.WriteLine(Find(a, 9));

            static int Find(ArrayNoSize a, int x)
            {
                int l = 0;
                int r = 0;

                if (a.ItemAt(0) == -1)
                    return -1;

                while (l > -1)
                {
                    int i = -1;

                    // optimization we can skip level where values are less than x
                    // because are ordered
                    if (x <= a.ItemAt(r))
                    {
                        i = Search(a, l, r, x);
                    }

                    if (i == -1)
                    {
                        l = r + 1;

                        if (a.ItemAt(l) == -1)
                            return -1;

                        r = l * 2;
                        while (a.ItemAt(r) == -1)
                        {
                            r = (l + r) / 2;
                        }
                    }
                    else
                        return i;
                }
                return -1;
            }

            static int Search(ArrayNoSize a, int l, int r, int x)
            {
                while (l <= r)
                {
                    int mid = (l + r) / 2;
                    if (a.ItemAt(mid) == x)
                        return mid;

                    if (a.ItemAt(mid) > x)
                        r = mid - 1;
                    else
                        l = mid + 1;
                }

                return -1;
            }

        }

        class ArrayNoSize
        {
            int[] _a;
            public ArrayNoSize(int[] a)
            {
                _a = a;
            }

            public int ItemAt(int i)
            {
                if (i < 0 || i > _a.Length - 1)
                    return -1;

                return _a[i];
            }

        }

        public static void SearchInRotatedArray()
        {
            int foundIndex = -1;
            int n = 19;
            int[] a = new int[] { 15, 16, 19, 20, 25, 25, 1, 3, 4, 5, 7, 10, 14 };

            int s = 0;

            for (int i = 0; i < a.Length - 2; i++)
            {
                if (a[i] - a[i + 1] > 0)
                {
                    s = i + 1;
                    break;
                }
            }

            int fs = 0;
            int fe = a.Length - 1;

            while (fs <= fe)
            {
                int mid = (fs + fe) / 2;

                if (a[(s + mid) % a.Length] > n)
                    fe = mid - 1;
                else if (a[(s + mid) % a.Length] < n)
                    fs = mid + 1;
                else
                {
                    foundIndex = (s + mid) % a.Length;
                    break;
                }
            }

            Console.WriteLine(foundIndex);

        }

        public static void GroupAnagrams2()
        {
            string[] a = new string[] { "ENAC", "EANC", "FRANCO", "BA", "NAEC", "OCNARF", "AB", "D" };
            // string[] a = new string[] { "EANC", "FRANCO", "ENAC", "EANC" };

            // Console.WriteLine(FirstInOrder("arco", "Marco"));
            // Console.WriteLine(FirstInOrder("arco", "arco"));
            // Console.WriteLine(FirstInOrder("arco", "arc"));
            // Console.WriteLine(FirstInOrder("rco", "arc"));
            // Console.WriteLine(FirstInOrder("Arco", "arc"));
            // Console.WriteLine(FirstInOrder("Marco", "Marco"));
            // Console.WriteLine(FirstInOrder("ENAC", "FRANCO"));
            // Console.WriteLine(FirstInOrder("NAEC", "FRANCO"));
            // Console.WriteLine(FirstInOrder("EANC", "FRANCO"));
            // Console.WriteLine(FirstInOrder("NAEC", "FRANCO"));

            QuickSortString(a, 0, a.Length - 1);

            foreach (var item in a)
            {
                Console.WriteLine(item);
            }

            return;

            static void QuickSortString(string[] array, int l, int r)
            {
                int i = Partition(array, l, r);
                if (l < i - 1)
                    QuickSortString(array, l, i - 1);
                if (i < r)
                    QuickSortString(array, i, r);
            }
        }

        static int Partition(string[] a, int l, int r)
        {
            string pivot = a[(l + r) / 2];
            while (l <= r)
            {
                while (FirstInOrder(QuickSortString(a[l]), QuickSortString(pivot)) == -1)
                    l++;

                while (FirstInOrder(QuickSortString(a[r]), QuickSortString(pivot)) == 1)
                    r--;

                if (l <= r)
                {
                    string tmp = a[r];
                    a[r] = a[l];
                    a[l] = tmp;
                    l++;
                    r--;
                }
            }
            return l;
        }

        // 0 equal
        // -1 a < b
        // 1  b < a
        static int FirstInOrder(string sa, string sb)
        {
            int ai = 0;
            int bi = 0;

            while (ai < sa.Length && bi < sb.Length)
            {
                if (sa[ai] > sb[bi])
                    return 1;
                else if (sa[ai] < sb[bi])
                    return -1;

                ai++;
                bi++;
            }

            if (sa.Length == sb.Length)
                return 0;
            else if (sa.Length < sb.Length)
                return -1;
            else
                return 1;
        }

        static void QuickSort(char[] array, int l, int r)
        {
            int i = Partition(array, l, r);
            if (l < i - 1)
                QuickSort(array, l, i - 1);
            if (i < r)
                QuickSort(array, i, r);

            static int Partition(char[] a, int l, int r)
            {
                char pivot = a[(l + r) / 2];
                while (l <= r)
                {
                    while (a[l] < pivot)
                        l++;
                    while (a[r] > pivot)
                        r--;

                    if (l <= r)
                    {
                        char tmp = a[r];
                        a[r] = a[l];
                        a[l] = tmp;
                        l++;
                        r--;
                    }
                }
                return l;
            }
        }

        static string QuickSortString(string s)
        {
            if (_anagramsCache.ContainsKey(s))
                return _anagramsCache[s];

            char[] sa = s.ToCharArray();
            QuickSort(sa, 0, sa.Length - 1);
            _anagramsCache.Add(s, new string(sa));

            return _anagramsCache[s];
        }

        static System.Collections.Generic.Dictionary<string, string> _anagramsCache = new System.Collections.Generic.Dictionary<string, string>();

        public static void GroupAnagrams()
        {
            string[] a = new string[] { "AB", "CD", "BA", "DC" };

            foreach (var item in a)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            Array.Sort(a, (a, b) =>
            {
                char[] ar = a.ToCharArray();
                char[] br = b.ToCharArray();
                Array.Sort(ar);
                Array.Sort(br);

                return StringComparer.InvariantCultureIgnoreCase.Compare(new string(ar), new string(br));
            });

            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }

        public static void SortedMerge()
        {
            int[] a = new int[] { 2, 4, 6, 0, 0, 0 };
            int[] b = new int[] { 1, 3, 5 };
            //int[] a = new int[] { 1, 2, 3, 0, 0, 0 };
            //int[] b = new int[] { 4, 5, 6 };

            // SortedMerge(a, b);
            SortedMerge_Pg396(a, b);

            foreach (var i in a)
            {
                Console.WriteLine(i);
            }

            return;

            static void SortedMerge_Pg396(int[] a, int[] b)
            {
                SortedMerge(a, b, a.Length - b.Length, b.Length);

                static void SortedMerge(int[] a, int[] b, int lenghtA, int lenghtB)
                {
                    int lastIndexA = lenghtA - 1;
                    int lastIndexB = lenghtB - 1;
                    int lastIndexMerged = lenghtA + lenghtB - 1;

                    while (lastIndexB >= 0)
                    {
                        if (lastIndexA >= 0 && a[lastIndexA] > b[lastIndexB])
                        {
                            a[lastIndexMerged] = a[lastIndexA];
                            lastIndexA--;
                        }
                        else
                        {
                            a[lastIndexMerged] = b[lastIndexB];
                            lastIndexB--;
                        }
                        lastIndexMerged--;
                    }
                }
            }

            static void SortedMerge(int[] a, int[] b)
            {
                int i = 0;
                int j = 0;
                int lenOfA = a.Length - b.Length;

                while (j < b.Length && i < lenOfA)
                {
                    if (b[j] < a[i])
                    {
                        for (int k = a.Length - 2; k >= i; k--)
                        {
                            a[k + 1] = a[k];
                        }
                        a[i] = b[j];
                        j++;
                        lenOfA++;
                        continue;
                    }
                    i++;
                }

                while (j < b.Length)
                {
                    a[i++] = b[j];
                    j++;
                }
            }


        }


        public static void Sorting()
        {
            int[] array = new int[] { 3, 4, 7, 8, 2, 9, -1, 1, -100, 34, 6, 4 };
            // int[] array = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            // int[] array = new int[] { 2, 1 };
            // int[] array = new int[] { 7, 1, 2, 3, 4 };
            // int[] array = new int[] { 7, 3, 5, 4, 2 };

            // BubbleSortWhileOptimized(array);
            // BubbleSortDoubleFor(array);
            // SelectionSort(array);
            // SelectionSortSwapAtTheEndOfCicle(array);
            // MergeSort(array, 0, array.Length - 1);
            // MergeSortJBEvans(array);

            // QuickSort(array);
            // QuickSort2(array, 0, array.Length - 1);
            // QuickSort3(array, 0, array.Length - 1);
            // QuickSort4(array, 0, array.Length - 1);
            RadixSort(array);
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }

            return;

            static void RadixSort(int[] array)
            {
                int startOfPositive = PartitionByZero(array);

                RadixSortInternal(array, startOfPositive, array.Length - 1);
                RadixSortInternal(array, 0, startOfPositive - 1);
                ReverseNegative(array, 0, startOfPositive - 1);

                void ReverseNegative(int[] array, int s, int e)
                {
                    while (s < e)
                    {
                        int tmp = array[s];
                        array[s] = array[e];
                        array[e] = tmp;
                        s++;
                        e--;
                    }
                }

                void RadixSortInternal(int[] array, int s, int e)
                {
                    if (s >= e)
                        return;

                    System.Collections.Generic.List<System.Collections.Generic.Queue<int>> buckets =
                    new System.Collections.Generic.List<System.Collections.Generic.Queue<int>>();

                    for (int i = 0; i < 10; i++)
                    {
                        buckets.Add(new System.Collections.Generic.Queue<int>());
                    }

                    int passes = MaxDigit(array, s, e);
                    for (int i = 1; i <= passes; i++)
                    {
                        for (int k = s; k <= e; k++)
                        {
                            int countDigit = CountDigit(array[k]);
                            if (countDigit >= i)
                            {
                                int digitVal = GetDigit(array[k], i - 1);
                                buckets[digitVal].Enqueue(array[k]);
                            }
                            else
                            {
                                buckets[0].Enqueue(array[k]);
                            }
                        }

                        int indexCount = s;
                        foreach (System.Collections.Generic.Queue<int> b in buckets)
                        {
                            while (b.Count > 0)
                            {
                                array[indexCount] = b.Dequeue();
                                indexCount++;
                            }
                        }
                    }
                }

                static int GetDigit(int n, int position)
                {
                    if (position == 0)
                        return Math.Abs(n) % 10;

                    return GetDigit(Math.Abs(n) / 10, position - 1);
                }


                static int MaxDigit(int[] array, int s, int e)
                {
                    int max = 0;
                    for (int i = s; i <= e; i++)
                    {
                        max = Math.Max(max, CountDigit(array[i]));
                    }
                    return max;
                }

                static int CountDigit(int n)
                {
                    return (int)Math.Log10(Math.Abs((double)n)) + 1;
                }

                static int PartitionByZero(int[] array)
                {
                    int l = 0;
                    int r = array.Length - 1;

                    while (l <= r)
                    {
                        while (l < array.Length && array[l] < 0)
                            l++;

                        while (r >= 0 && array[r] > 0)
                            r--;

                        if (l <= r)
                        {
                            Swap(array, l, r);
                            l++;
                            r--;
                        }
                    }
                    return l;
                }

                static void Swap(int[] array, int a, int b)
                {
                    int tmp = array[a];
                    array[a] = array[b];
                    array[b] = tmp;
                }
            }

            static void QuickSort4(int[] array, int left, int right)
            {
                int index = Partition(array, left, right);

                if (left < index - 1)
                    QuickSort4(array, left, index - 1);

                if (index < right)
                    QuickSort4(array, index, right);
            }

            static int Partition(int[] array, int left, int right)
            {
                int pivot = (left + right) / 2;

                while (left <= right)
                {
                    while (array[left] < array[pivot])
                        left++;

                    while (array[right] > array[pivot])
                        right--;

                    if (left <= right)
                    {
                        Swap(array, left, right);
                        left++;
                        right--;
                    }
                }

                return left;
            }

            static void Swap(int[] array, int a, int b)
            {
                int tmp = array[a];
                array[a] = array[b];
                array[b] = tmp;
            }
        }

        static void QuickSort3(int[] array, int l, int h)
        {

            if (l < h)
            {
                int position = Partition(array, l, h);
                QuickSort3(array, l, position - 1);
                QuickSort3(array, position + 1, h);
            }

            static int Partition(int[] array, int l, int h)
            {
                int pivot = array[h];
                int i = l;

                for (int j = l; j < h; j++)
                {
                    if (array[j] < pivot)
                    {
                        Swap(array, j, i);
                        i++;
                    }
                }
                Swap(array, h, i);
                return i;
            }

            static void Swap(int[] array, int a, int b)
            {
                int tmp = array[a];
                array[a] = array[b];
                array[b] = tmp;
            }
        }

        // https://www.youtube.com/watch?v=7h1s2SojIRw
        static void QuickSort2(int[] array, int l, int r)
        {
            if (l < r)
            {
                int pivotPosition = Partition(array, l, r);
                QuickSort2(array, l, pivotPosition - 1);
                QuickSort2(array, pivotPosition + 1, r);
            }

            static int Partition(int[] array, int l, int r)
            {
                int i = l;
                int j = r;

                int pivotValue = array[l];

                while (i < j)
                {
                    for (; i < r; i++)
                    {
                        if (array[i] > pivotValue)
                            break;
                    }

                    for (; j >= i; j--)
                    {
                        if (array[j] < pivotValue)
                            break;
                    }

                    if (i < j)
                    {
                        Swap(array, i, j);
                        i++;
                        j--;
                    }
                }

                Swap(array, l, j);
                return j;
            }

            static void Swap(int[] array, int a, int b)
            {
                int tmp = array[a];
                array[a] = array[b];
                array[b] = tmp;
            }
        }

        static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);

            static void QuickSort(int[] array, int left, int right)
            {
                int index = Partition(array, left, right);
                if (left < index - 1)
                {
                    QuickSort(array, left, index - 1);
                }
                if (index < right)
                {
                    QuickSort(array, index, right);
                }
            }

            static int Partition(int[] array, int left, int right)
            {
                int pivot = (left + right) / 2;

                while (left <= right)
                {
                    while (array[left] < array[pivot])
                        left++;

                    while (array[right] > array[pivot])
                        right--;

                    if (left <= right)
                    {
                        Swap(array, left, right);
                        left++;
                        right--;
                    }
                }

                return left;
            }

            static void Swap(int[] array, int a, int b)
            {
                int tmp = array[a];
                array[a] = array[b];
                array[b] = tmp;
            }
        }


        static void MergeSortJBEvans(int[] array)
        {
            MergeSortClass.Sort(array);
        }

        static void MergeSort(int[] array, int s, int e)
        {
            if (s < e)
            {
                int middle = (s + e) / 2;
                MergeSort(array, 0, middle);
                MergeSort(array, middle + 1, e);
                Merge(array, s, e, middle);
            }
        }

        static void Merge(int[] array, int s, int e, int middle)
        {
            int[] leftArray = new int[(middle - s) + 1];
            Array.Copy(array, s, leftArray, 0, leftArray.Length);
            int[] rightArray = new int[e - middle];
            Array.Copy(array, middle + 1, rightArray, 0, rightArray.Length);

            int leftArrayIndex = 0;
            int rightArrayIndex = 0;
            int index = s;
            while (leftArrayIndex < leftArray.Length && rightArrayIndex < rightArray.Length)
            {
                if (leftArray[leftArrayIndex] <= rightArray[rightArrayIndex])
                {
                    array[index] = leftArray[leftArrayIndex];
                    leftArrayIndex++;
                }
                else
                {
                    array[index] = rightArray[rightArrayIndex];
                    rightArrayIndex++;
                }
                index++;
            }

            while (leftArrayIndex < leftArray.Length)
            {
                array[index] = leftArray[leftArrayIndex];
                index++;
                leftArrayIndex++;
            }

            // If left array is ok right remains will be already ordered by precedence recoursion 
            //while (rightArrayIndex < rightArray.Length)
            //{
            //    array[index] = rightArray[rightArrayIndex];
            //    index++;
            //    rightArrayIndex++;
            //}
        }

        static void SelectionSortSwapAtTheEndOfCicle(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[minIndex] > array[j])
                    {
                        minIndex = j;
                    }
                }

                int tmp = array[minIndex];
                array[minIndex] = array[i];
                array[i] = tmp;
            }
        }

        static void SelectionSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int min = array[i];
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (min > array[j])
                    {
                        int tmp = array[j];
                        array[j] = min;
                        min = tmp;
                    }
                }
                array[i] = min;
            }
        }

        static void BubbleSortDoubleFor(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)      // N 
            {                                               // *
                for (int k = 0; k < array.Length - 1; k++)  // N
                {
                    if (array[k] > array[k + 1])
                    {
                        int tmp = array[k];
                        array[k] = array[k + 1];
                        array[k + 1] = tmp;
                    }
                }
            }
        }

        static void BubbleSortWhileOptimized(int[] array)
        {
            int count = array.Length - 1;
            bool isSorted = false;
            while (!isSorted)
            {
                int lastSwap = -1;
                isSorted = true;
                for (int i = 0; i < count; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        int tmp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = tmp;
                        isSorted = false;
                        lastSwap = i;
                    }
                }
                count = lastSwap != -1 ? lastSwap : count;
            }
        }
    }

    class MergeSortClass
    {
        private readonly int[] elements;
        private readonly int[] buffer;

        public MergeSortClass(int[] elements)
        {
            this.elements = elements;
            this.buffer = new int[elements.Length];
            Array.Copy(this.elements, this.buffer, elements.Length);
        }

        public static void Sort(int[] source)
        {
            Sort(source, 0, source.Length);
        }

        public static void Sort(int[] source, int start, int length)
        {
            new MergeSortClass(source).Sort(start, length);
        }

        private void Sort(int start, int length)
        {
            TopDownSplitMerge(this.buffer, this.elements, start, length);
        }

        private void TopDownSplitMerge(int[] buffer, int[] elements, int start, int end)
        {
            if (end - start < 2)
                return;

            int middle = (end + start) / 2;
            TopDownSplitMerge(elements, buffer, start, middle);
            TopDownSplitMerge(elements, buffer, middle, end);
            TopDownMerge(buffer, elements, start, middle, end);
        }

        private void TopDownMerge(int[] buffer, int[] elements, int start, int middle, int end)
        {
            for (int i = start, j = middle, k = start; k < end; k++)
            {
                if (i < middle && (j >= end || buffer[i] <= buffer[j]))
                {
                    elements[k] = buffer[i++];
                }
                else
                {
                    elements[k] = buffer[j++];
                }
            }
        }
    }
}

