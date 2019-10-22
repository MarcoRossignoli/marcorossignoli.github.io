using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    class Geeksforgeeks
    {
        public static void QueueWithLinkedList()
        {
            Queue q = new Queue();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);

            Console.WriteLine(q.Dequeue());
            Console.WriteLine(q.Dequeue());
            Console.WriteLine(q.Dequeue());
            Console.WriteLine(q.Dequeue());

            Console.WriteLine(q.Dequeue());
        }


        public class Queue
        {
            class QueueNode
            {
                public QueueNode(int val)
                {
                    Val = val;
                }

                public int Val { get; set; }
                public QueueNode Next { get; set; }
            }

            QueueNode _front;
            QueueNode _tail;

            public void Enqueue(int val)
            {
                if (_front is null)
                {
                    _front = new QueueNode(val);
                    _tail = _front;
                }
                else
                {
                    QueueNode n = new QueueNode(val);
                    _tail.Next = n;
                    _tail = n;
                }
            }

            public int Dequeue()
            {
                if (_front is null)
                    throw new Exception("empty");

                QueueNode node = _front;
                _front = _front.Next;

                if (_front is null)
                    _tail = null;

                return node.Val;
            }

        }
    }
}
