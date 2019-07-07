using System;
using System.Linq;

namespace Interview
{
    public class SearchingAlg
    {
        public static void BinarySearch()
        {
            ObjToSearch[] values = Enumerable.Range(1, 11).Select(i => new ObjToSearch() { Value = i, ValueStr = i.ToString() }).ToArray();

            Console.WriteLine(BinarySearch(values, 11).ValueStr);

            return;

            static ObjToSearch BinarySearch(ObjToSearch[] list, int value)
            {
                int low = 0;
                int high = list.Length - 1;

                while (low <= high)
                {
                    int mid = (low + high) / 2;
                    if (list[mid].Value < value)
                    {
                        low = mid + 1;
                    }
                    else if (list[mid].Value > value)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        return list[mid];
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
