using System;

namespace Interview
{
    public class Lists_Queue
    {
        public static void QueueTest()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < 100; i++)
            {
                queue.Add(i);
            }

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(queue.Remove());
            }
        }
    }

    public class Queue<T>
    {
        QueueNode<T> _first;
        QueueNode<T> _last;

        public void Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            QueueNode<T> node = new QueueNode<T>() { Data = item };

            if (_last != null)
            {
                _last.Next = node;
            }

            _last = node;

            if (_first is null)
            {
                _first = _last;
            }
        }

        public T Remove()
        {
            if (_first is null)
            {
                throw new InvalidOperationException("Is empty");
            }

            T data = _first.Data;
            _first = _first.Next;

            if (_first is null)
            {
                _last = null;
            }

            return data;
        }

        public T Peek()
        {
            if (_first is null)
            {
                throw new InvalidOperationException("Is empty");
            }

            return _first.Data;
        }


        public bool IsEmpty()
        {
            return _first == null;
        }

        class QueueNode<TValue>
        {
            public TValue Data { get; set; }
            public QueueNode<TValue> Next { get; set; }
        }
    }
}
