using System;
using System.Linq;

namespace Interview
{
    public class SearchingAlg
    {
        public static void BinarySearch_Recoursive()
        {
            ObjToSearch[] values = Enumerable.Range(1, 10).Select(i => new ObjToSearch() { Value = i, ValueStr = i.ToString() }).ToArray();

            Console.WriteLine(BinaryRecoursiveSearch(values, 8, 0, values.Length - 1).ValueStr);

            return;

            static ObjToSearch BinaryRecoursiveSearch(ObjToSearch[] array, int value, int low, int high)
            {
                if (low > high)
                {
                    throw new Exception($"{value} not found");
                }

                int mid = (low + high) / 2;

                if (array[mid].Value == value)
                {
                    return array[mid];
                }
                else if (value > array[mid].Value)
                {
                    return BinaryRecoursiveSearch(array, value, mid + 1, high);
                }
                else
                {
                    return BinaryRecoursiveSearch(array, value, low, high - 1);
                }
            }
        }

        public static void BinarySearch()
        {
            ObjToSearch[] values = Enumerable.Range(1, 10).Select(i => new ObjToSearch() { Value = i, ValueStr = i.ToString() }).ToArray();

            Console.WriteLine(BinarySearch(values, 8).ValueStr);

            return;

            static ObjToSearch BinarySearch(ObjToSearch[] list, int value)
            {
                int low = 1;
                int high = list.Length - 1;

                while (low <= high)
                {
                    int mid = (low + high) / 2;
                    if (list[mid].Value == value)
                    {
                        return list[mid];
                    }
                    else if (value > list[mid].Value)
                    {
                        low = mid + 1;
                    }
                    else
                    {
                        high = mid - 1;
                    }
                }

                throw new Exception($"{value} not found");
            }

        }
    }

    public class ObjToSearch
    {
        public int Value { get; set; }
        public string ValueStr { get; set; }
    }
}
