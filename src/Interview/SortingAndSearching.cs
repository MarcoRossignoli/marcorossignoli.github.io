using System;

namespace Interview
{
    class SortingAndSearching
    {
        public static void GroupAnagrams2()
        {
            string[] a = new string[] { "AB", "CD", "BA", "DC" };

            //Console.WriteLine(FirstInOrder("arco", "Marco"));
            //Console.WriteLine(FirstInOrder("arco", "arco"));
            //Console.WriteLine(FirstInOrder("arco", "arc"));
            //Console.WriteLine(FirstInOrder("rco", "arc"));
            //Console.WriteLine(FirstInOrder("Arco", "arc"));
            //Console.WriteLine(FirstInOrder("Marco", "Marco"));

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
                    Partition(array, l, i - 1);
                if (i < r)
                    Partition(array, i, r);


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
            char[] a = sa.ToCharArray();
            char[] b = sb.ToCharArray();

            int ai = 0;
            int bi = 0;

            while (ai < a.Length && bi < b.Length)
            {
                if (a[ai] > b[bi])
                    return 1;
                else if (a[ai] < b[bi])
                    return -1;

                ai++;
                bi++;
            }

            if (a.Length == b.Length)
                return 0;
            else if (a.Length < b.Length)
                return -1;
            else
                return 1;
        }

        static void QuickSort(char[] array, int l, int r)
        {
            int i = Partition(array, l, r);
            if (l < i - 1)
                Partition(array, l, i - 1);
            if (i < r)
                Partition(array, i, r);

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
            char[] sa = s.ToCharArray();
            QuickSort(sa, 0, sa.Length - 1);
            return new string(sa);
        }

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

