using System;
using System.Collections.Generic;

namespace Interview
{
    public class Sorting
    {
        public static void RadixSort()
        {
            int[] array = new int[] { 38, 27, 43, 3, 9, 82, 102 };

            PrintArray(array, "Radix sort pre");

            RadixSort(array);

            PrintArray(array, "Radix sort post");

            static int[] RadixSort(int[] array)
            {
                bool isFinished = false;
                int digitPosition = 0;

                List<System.Collections.Generic.Queue<int>> buckets = new List<System.Collections.Generic.Queue<int>>();
                InitializeBuckets(buckets);

                while (!isFinished)
                {
                    isFinished = true;

                    foreach (int value in array)
                    {
                        int bucketNumber = GetBucketNumber(value, digitPosition);
                        if (bucketNumber > 0)
                        {
                            isFinished = false;
                        }

                        buckets[bucketNumber].Enqueue(value);
                    }

                    int i = 0;
                    foreach (System.Collections.Generic.Queue<int> bucket in buckets)
                    {
                        while (bucket.Count > 0)
                        {
                            array[i] = bucket.Dequeue();
                            i++;
                        }
                    }

                    digitPosition++;
                }

                return array;
            }

            static int GetBucketNumber(int value, int digitPosition)
            {
                int bucketNumber = (value / (int)Math.Pow(10, digitPosition)) % 10;
                return bucketNumber;
            }

            static void InitializeBuckets(List<System.Collections.Generic.Queue<int>> buckets)
            {
                for (int i = 0; i < 10; i++)
                {
                    System.Collections.Generic.Queue<int> q = new System.Collections.Generic.Queue<int>();
                    buckets.Add(q);
                }
            }
        }

        public static void QuickSort()
        {
            int[] array = new int[] { 4, 3, 7, 5, 2 };

            PrintArray(array, "Quick sort pre");

            QuickSort(array, 0, array.Length - 1);

            PrintArray(array, "Quick sort post");

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
                int pivot = array[(left + right) / 2];
                while (left <= right)
                {
                    while (array[left] < pivot)
                        left++;

                    while (array[right] > pivot)
                        right--;

                    if (left <= right)
                    {
                        int tmp = array[left];
                        array[left] = array[right];
                        array[right] = tmp;

                        left++;
                        right--;
                    }
                }
                return left;
            }

        }

        public static void MergeSort()
        {
            int[] array = new int[] { 38, 27, 43, 3, 9, 82, 10 };

            //Random r = new Random();
            //for (int i = 0; i < array.Length; i++)
            //{
            //    array[i] = r.Next(0, 20);
            //}

            PrintArray(array, "Merge sort pre");

            MergeSortTop(array);

            PrintArray(array, "Merge sort post");

            static void MergeSortTop(int[] array)
            {
                int[] helper = new int[array.Length];
                MergeSort(array, helper, 0, array.Length - 1);
            }

            static void MergeSort(int[] array, int[] helper, int low, int high)
            {
                if (low < high)
                {
                    int middle = (low + high) / 2;
                    MergeSort(array, helper, low, middle);
                    MergeSort(array, helper, middle + 1, high);
                    Merge(array, helper, low, middle, high);
                }
            }

            static void Merge(int[] array, int[] helper, int low, int middle, int high)
            {
                for (int i = low; i <= high; i++)
                {
                    helper[i] = array[i];
                }

                int helperLeft = low;
                int helperRight = middle + 1;
                int current = low;

                while (helperLeft <= middle && helperRight <= high)
                {
                    if (helper[helperLeft] <= helper[helperRight])
                    {
                        array[current] = helper[helperLeft];
                        helperLeft++;
                    }
                    else
                    {
                        array[current] = helper[helperRight];
                        helperRight++;
                    }

                    current++;
                }

                int remaining = middle - helperLeft;
                for (int i = 0; i <= remaining; i++)
                {
                    array[current + i] = helper[helperLeft + i];
                }
            }
        }

        public static void SelectionSort()
        {
            int[] array = new int[13];
            Random r = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = r.Next(0, 20);
            }

            PrintArray(array, "Selection sort pre");

            int minIndex;
            int tmp;
            for (int i = 0; i < array.Length; i++)
            {
                minIndex = i;

                for (int k = i + 1; k < array.Length; k++)
                {
                    if (array[minIndex] > array[k])
                    {
                        minIndex = k;
                    }
                }

                if (minIndex != i)
                {
                    tmp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = tmp;
                }
            }

            PrintArray(array, "Selection sort post");
        }

        public static void BubbleSort()
        {
            int[] array = new int[13];
            Random r = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = r.Next(0, 20);
            }

            PrintArray(array, "Bubble sort pre");

            int tmp;
            int n = array.Length - 1;
            while (n > 0)
            {
                int lastSwapIndex = -1;

                // i < n because we stop on second to last
                for (int i = 0; i < n; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        tmp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = tmp;
                        lastSwapIndex = i;
                    }
                }

                if (lastSwapIndex != -1)
                {
                    n = lastSwapIndex;
                }
                else
                {
                    break;
                }
            }

            PrintArray(array, "Bubble sort after");
        }

        private static void PrintArray(int[] list, string label)
        {
            Console.WriteLine($"----{label}----");
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(list[i]);
            }
        }
    }
}
