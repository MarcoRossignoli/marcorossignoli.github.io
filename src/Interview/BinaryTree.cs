using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Interview
{
    public class TreeAndGraphs_BinaryTree
    {
        public static void BinaryTree_Insertion()
        {
            TreeNode<int> root = null;

            root = Insert(root, 10);
            root = Insert(root, 20);
            root = Insert(root, 30);
            root = Insert(root, 40);
            root = Insert(root, 50);
            root = Insert(root, 60);
            root = Insert(root, 70);

            List<TreeNode<int>> nodes = new List<TreeNode<int>>();

            StoreNodeInOrder(nodes, root);

            MergeSortTop(nodes);

            root = Rebalance(nodes, 0, nodes.Count - 1);

            return;

            static void MergeSortTop(List<TreeNode<int>> nodes)
            {
                TreeNode<int>[] helper = new TreeNode<int>[nodes.Count];
                MergeSort(nodes, helper, 0, nodes.Count - 1);
            }

            static void MergeSort(List<TreeNode<int>> nodes, TreeNode<int>[] helper, int low, int high)
            {
                if (low < high)
                {
                    int mid = (low + high) / 2;
                    MergeSort(nodes, helper, low, mid);
                    MergeSort(nodes, helper, mid + 1, high);
                    Merge(nodes, helper, low, mid, high);
                }
            }

            static void Merge(List<TreeNode<int>> nodes, TreeNode<int>[] helper, int low, int middle, int high)
            {
                for (int i = low; i <= high; i++)
                {
                    helper[i] = nodes[i];
                }

                int leftHelper = low;
                int rightHelper = middle + 1;
                int current = low;

                while (leftHelper <= middle && rightHelper <= high)
                {
                    if (helper[leftHelper].Data < helper[rightHelper].Data)
                    {
                        nodes[current] = helper[leftHelper];
                        leftHelper++;
                    }
                    else
                    {
                        nodes[current] = helper[rightHelper];
                        rightHelper++;
                    }
                    current++;
                }

                int remaining = middle - leftHelper;
                for (int i = 0; i <= remaining; i++)
                {
                    nodes[current + i] = helper[leftHelper + i];
                }

            }

            static TreeNode<int> Rebalance(List<TreeNode<int>> nodes, int start, int end)
            {
                if (start > end)
                {
                    return null;
                }

                int mid = (start + end) / 2;
                TreeNode<int> node = nodes[mid];

                node.Left = Rebalance(nodes, start, mid - 1);
                node.Right = Rebalance(nodes, mid + 1, end);

                return node;
            }

            static void StoreNodeInOrder(List<TreeNode<int>> nodes, TreeNode<int> root)
            {
                if (root is null)
                {
                    return;
                }

                StoreNodeInOrder(nodes, root.Left);
                nodes.Add(root);
                StoreNodeInOrder(nodes, root.Right);
            }

            static TreeNode<int> Insert(TreeNode<int> root, int value)
            {
                if (root == null)
                {
                    return new TreeNode<int>() { Data = value };
                }

                if (value > root.Data)
                {
                    root.Left = Insert(root.Left, value);
                }
                else
                {
                    root.Right = Insert(root.Right, value);
                }

                return root;
            }
        }

        public static void BinaryTree_Visiting()
        {
            int n = 10;
            TreeNode<int> node = new TreeNode<int>() { Data = n / 2 };

            for (int i = 1; i < n; i++)
            {
                if (i == n / 2)
                    continue;

                Insert(node, i);
            }

            Console.WriteLine("In Order");
            InOrderTraversal(node);
            Console.WriteLine("-------");

            Console.WriteLine("Pre Order");
            PreOrderTraversal(node);
            Console.WriteLine("-------");

            Console.WriteLine("Post Order");
            PostOrderTraversal(node);
            Console.WriteLine("-------");

            return;

            static TreeNode<T> Insert<T>(TreeNode<T> node, T value)
            {
                if (node == null)
                {
                    node = new TreeNode<T>() { Data = value };
                }
                else if (Comparer<T>.Default.Compare(value, node.Data) <= 0)
                {
                    node.Left = Insert(node.Left, value);
                }
                else if (Comparer<T>.Default.Compare(value, node.Data) > 0)
                {
                    node.Right = Insert(node.Right, value);
                }
                return node;
            }

            static void InOrderTraversal<T>(TreeNode<T> node)
            {
                if (node != null)
                {
                    InOrderTraversal(node.Left);
                    Visit(node);
                    InOrderTraversal(node.Right);
                }
            }

            static void PreOrderTraversal<T>(TreeNode<T> node)
            {
                if (node != null)
                {
                    Visit(node);
                    InOrderTraversal(node.Left);
                    InOrderTraversal(node.Right);
                }
            }

            static void PostOrderTraversal<T>(TreeNode<T> node)
            {
                if (node != null)
                {
                    InOrderTraversal(node.Left);
                    InOrderTraversal(node.Right);
                    Visit(node);
                }
            }

            static void Visit<T>(TreeNode<T> node)
            {
                if (node != null)
                {
                    Console.WriteLine(node.Data);
                }
            }
        }
    }

    [DebuggerDisplay("Data = {Data}")]
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
    }
}
