using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Interview
{
    public class TreeAndGraphs_Graphs
    {
        private static Graph<int> LoadGraph()
        {
            Graph<int> graph = new Graph<int>();
            GraphNode<int> node0 = new GraphNode<int>() { Value = 0 };
            GraphNode<int> node1 = new GraphNode<int>() { Value = 1 };
            GraphNode<int> node2 = new GraphNode<int>() { Value = 2 };
            GraphNode<int> node3 = new GraphNode<int>() { Value = 3 };
            GraphNode<int> node4 = new GraphNode<int>() { Value = 4 };
            GraphNode<int> node5 = new GraphNode<int>() { Value = 5 };

            node0.Children = new GraphNode<int>[] { node1, node4, node5 };
            node1.Children = new GraphNode<int>[] { node3, node4 };
            node2.Children = new GraphNode<int>[] { node1 };
            node3.Children = new GraphNode<int>[] { node2, node4 };

            graph.Nodes = node0;

            return graph;
        }
        public static void Graph_DFS()
        {
            HashSet<GraphNode<int>> visited = new HashSet<GraphNode<int>>();

            Graph<int> graph = LoadGraph();

            DFS(graph.Nodes, visited);

            return;

            static void DFS<T>(GraphNode<T> node, HashSet<GraphNode<T>> visited)
            {
                if (node == null)
                {
                    return;
                }
                Visit(node, visited);
                if (node.Children != null)
                {
                    foreach (GraphNode<T> child in node.Children)
                    {
                        if (!visited.Contains(child))
                        {
                            DFS(child, visited);
                        }
                    }
                }
            }
        }

        public static void Graph_BFS()
        {
            HashSet<GraphNode<int>> visited = new HashSet<GraphNode<int>>();

            Graph<int> graph = LoadGraph();

            BFS(graph.Nodes, visited);

            return;

            static void BFS<T>(GraphNode<T> root, HashSet<GraphNode<T>> visited)
            {
               Queue<GraphNode<T>> queue = new Queue<GraphNode<T>>();
                queue.Add(root);
                while (!queue.IsEmpty())
                {
                    GraphNode<T> node = queue.Remove();
                    Visit(node, visited);
                    for (int i = 0; node.Children != null && i < node.Children.Length; i++)
                    {
                        if(!visited.Contains(node.Children[i]))
                        {
                            visited.Add(node.Children[i]);
                            queue.Add(node.Children[i]);
                        }
                    }
                }
            }
        }

        static void Visit<T>(GraphNode<T> node, HashSet<GraphNode<T>> visited)
        {
            Console.WriteLine($"Visit {node.Value}");
        }
    }

    public class Graph<T>
    {
        public GraphNode<T> Nodes { get; set; }
    }

    [DebuggerDisplay("Display = {Value}")]
    public class GraphNode<T>
    {
        public T Value { get; set; }
        public GraphNode<T>[] Children { get; set; }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
