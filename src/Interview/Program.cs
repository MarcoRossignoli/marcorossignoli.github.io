using System.Threading.Tasks;

namespace Interview
{
    class Program
    {
        async static Task Main(string[] args)
        {
            // BigO.Permutation();
            // BigO.FibonacciNth();
            // BigO.PrintFibonacci();
            // BigO.Factorial();
            // BigO.FactorialRecoursive();

            // ArrayAndString.ArrayAndString.TestArrayList();
            // ArrayAndString.ArrayAndString_HashTable.TestHashTable();
            // ArrayAndString.ArrayAndString_StringBuilder.TestStringbuilder();
            // ArrayAndString.ArrayAndString_Exercise.IsUnique();
            // ArrayAndString.ArrayAndString_Exercise.Permutation();

            // Lists_LinkedList.LinkedListTest();
            // Lists_Stack.StackTest();
            // Lists_Queue.QueueTest();

            // TreeAndGraphs_BinaryTree.BinaryTree_Visiting();
            // TreeAndGraphs_Graphs.Graph_DFS();
            // TreeAndGraphs_Graphs.Graph_BFS();

            // BitManipulations.Test();

            // Sorting.BubbleSort();
            // Sorting.SelectionSort();
            // Sorting.MergeSort();
            // Sorting.QuickSort();
            // Sorting.RadixSort();

            // SearchingAlg.BinarySearch();
            // SearchingAlg.BinarySearch_Recoursive();

            // Games.BasicCalculator.Calculator();
            // Games.JumpGame.Play();

            // await Http11.Http1Test.Http11Test();
            await Http2.Http2Test.Test();
        }
    }
}
