using System;

namespace Interview
{
    public class Sorting
    {
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
            int n = array.Length;
            while (true)
            {
                bool exit = true;

                // we stop on second to last
                for (int i = 0; i < n - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        tmp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = tmp;
                        exit = false;
                    }
                }

                // at every pass last greatest value are in right place so we can skip next cycle
                n = n - 1;

                if (exit)
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
