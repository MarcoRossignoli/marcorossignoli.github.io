using System;

namespace Interview
{
    public class Lists_LinkedList
    {
        public static void LinkedListTest()
        {
            LinkedList<int> ll = new LinkedList<int>();
            for (int i = 0; i < 100; i++)
            {
                ll.Append(i);
            }

            Console.WriteLine($"Count {ll.Count}");

            ll.Remove(50);
        }
    }

    public class LinkedList<T>
    {
        public Node<T> Head { get; set; }

        public void Append(T value)
        {
            Node<T> node = new Node<T>() { Data = value };

            if (Head is null)
            {
                Head = node;
            }
            else
            {
                Node<T> currentNode = Head;
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = node;
            }
        }

        public int Count
        {
            get
            {
                if (Head is null)
                {
                    return 0;
                }

                int count = 1;

                Node<T> currentNode = Head;
                while (currentNode.Next != null)
                {
                    count++;
                    currentNode = currentNode.Next;
                }

                return count;
            }
        }

        public void Remove(int value)
        {
            if (Head != null)
            {
                Node<T> currentNode = Head;

                if (currentNode.Data.Equals(value))
                {
                    Head = currentNode.Next;
                }
                else
                {
                    while (currentNode.Next != null)
                    {
                        if(currentNode.Next.Data.Equals(value))
                        {
                            currentNode.Next = currentNode.Next.Next;
                            return;
                        }
                        currentNode = currentNode.Next;
                    }
                }
            }
        }

    }

    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
    }

}
