using System;

namespace Interview
{
    class SortingAndSearching
    {
        public static void Sorting()
        {
            int[] array = new int[] { 3, 4, 7, 8, 2, 9, -1, 1, -100, 34, 6, 4 };
            //int[] array = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            // BubbleSortWhileOptimized(array);
            // BubbleSortDoubleFor(array);
            // SelectionSort(array);
            // SelectionSortSwapAtTheEndOfCicle(array);
            MergeSort(array, 0, array.Length - 1);
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }

            return;

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
    }
}
