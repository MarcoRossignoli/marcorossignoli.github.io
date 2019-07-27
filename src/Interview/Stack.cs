using System;

namespace Interview
{
    public class Lists_Stack
    {
        public static void StackTest()
        {
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < 100; i++)
            {
                stack.Push(i);
            }

            Console.WriteLine(stack.Peek());

            while (!stack.IsEmpty())
            {
                Console.WriteLine(stack.Pop());
            }
        }
    }

    public class Stack<T>
    {
        StackNode<T> _head = null;

        public void Push(T value)
        {
            StackNode<T> node = new StackNode<T>() { Value = value };
            node.Next = _head;
            _head = node;
        }

        public T Pop()
        {
            if (_head is null)
            {
                throw new InvalidOperationException("Empty");
            }

            T value = _head.Value;

            _head = _head.Next;

            return value;

        }

        public T Peek()
        {
            if (_head is null)
            {
                throw new InvalidOperationException("Empty");
            }

            return _head.Value;
        }

        public bool IsEmpty()
        {
            return _head == null;
        }

        class StackNode<TValue>
        {
            public TValue Value { get; set; }
            public StackNode<TValue> Next { get; set; }
        }
    }
}
