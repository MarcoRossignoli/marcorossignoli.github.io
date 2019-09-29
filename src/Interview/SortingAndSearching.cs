using System;

namespace Interview
{
    class SortingAndSearching
    {
        public static void Sorting()
        {
            int[] array = new int[] { 3, 4, 7, 8, 2, 9, -1, 1, -100, 34, 6, 4 };

            // BubbleSortWhileOptimized(array);
            // BubbleSortDoubleFor(array);
            // SelectionSort(array);
            SelectionSortSwapAtTheEndOfCicle(array);
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }

            return;

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
    }
}
