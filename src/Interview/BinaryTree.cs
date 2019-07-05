using System;
using System.Collections.Generic;

namespace Interview
{
    public class TreeAndGraphs_BinaryTree
    {
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

    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
    }
}
