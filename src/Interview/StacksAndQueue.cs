using System;
using System.Collections.Generic;
using System.Diagnostics;
using fxStack = System.Collections.Generic.Stack<int>;
namespace Interview
{
    public class StacksAndQueue
    {
        public static void CircularQueue()
        {
            Queue<int> q = new Queue<int>();

            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);
            q.Enqueue(5);

            Console.WriteLine(q.Dequeue());
            Console.WriteLine(q.Dequeue());

            q.Enqueue(6);
            q.Enqueue(7);
            q.Enqueue(8);
            q.Enqueue(9);

            Console.WriteLine(q.Dequeue());
            Console.WriteLine(q.Dequeue());

            q.Enqueue(10);
            q.Enqueue(11);
            q.Enqueue(12);
            q.Enqueue(13);

            q.Enqueue(14);
            q.Enqueue(15);
        }

        class CircularQueue<T>
        {
            T[] _array = new T[5];
            int _current = 0;
            int _next = 0;
            int _count = 0;

            public void Enqueue(T val)
            {
                if (_count == _array.Length)
                {
                    Expand();
                }

                _array[_next] = val;
                _count++;
                _next = (_next + 1) % _array.Length;
            }

            void Expand()
            {
                T[] n = new T[_array.Length * 2];
                Array.Copy(_array, _current, n, 0, _array.Length - _current);
                Array.Copy(_array, 0, n, _array.Length - _current, _current);
                _current = 0;
                _next = _count;
                _array = n;
            }

            public T Dequeue()
            {
                if (_count == 0)
                {
                    throw new Exception("empty");
                }
                T val = _array[_current];
                _count--;
                _current = (_current + 1) % _array.Length;
                return val;
            }

        }

        public static void AnimalShelterBookIdeaPg239()
        {
            AnimalShelter3 ash = new AnimalShelter3();
            ash.Enque(new Dog() { Num = 1 });
            ash.Enque(new Cat() { Num = 2 });
            ash.Enque(new Cat() { Num = 3 });
            ash.Enque(new Dog() { Num = 4 });
            ash.Enque(new Cat() { Num = 5 });

            Console.WriteLine(ash.DequeueAny());
            Console.WriteLine(ash.DequeueAny());
            Console.WriteLine(ash.DequeueCat());
            Console.WriteLine(ash.DequeueDog());
            Console.WriteLine(ash.DequeueCat());

            Console.WriteLine(ash.DequeueCat());
            Console.WriteLine(ash.DequeueDog());
        }

        class AnimalShelter3
        {
            System.Collections.Generic.LinkedList<Dog> _dogs = new System.Collections.Generic.LinkedList<Dog>();
            System.Collections.Generic.LinkedList<Cat> _cats = new System.Collections.Generic.LinkedList<Cat>();
            int timestamp = 0;

            public void Enque(Animal2 a)
            {
                a.Timestamp = ++timestamp;
                if (a is Dog)
                    _dogs.AddLast((Dog)a);
                else
                    _cats.AddLast((Cat)a);
            }

            public Animal2 DequeueAny()
            {
                if (_cats.Count + _dogs.Count == 0)
                    throw new Exception("empty");
                if (_cats.Count > 0 && _dogs.Count == 0)
                {
                    return DequeueCat();
                }
                if (_dogs.Count > 0 && _cats.Count == 0)
                {
                    return DequeueDog();
                }
                if (_dogs.First.Value.Timestamp < _cats.First.Value.Timestamp)
                {
                    return DequeueDog();
                }
                else
                {
                    return DequeueCat();
                }
            }
            public Cat DequeueCat()
            {
                if (_cats.Count == 0)
                    throw new Exception("empty");
                Cat cat = _cats.First.Value;
                _cats.RemoveFirst();
                return cat;
            }
            public Dog DequeueDog()
            {
                if (_dogs.Count == 0)
                    throw new Exception("empty");
                Dog dog = _dogs.First.Value;
                _dogs.RemoveFirst();
                return dog;
            }
        }


        class Dog : Animal2 { }
        class Cat : Animal2 { }

        class Animal2
        {
            public int Num { get; set; }
            public int Timestamp { get; set; }
            public override string ToString()
            {
                return $"{this.GetType().Name} - {Num}";
            }
        }

        public static void AnimalShelterMy2()
        {
            AnimalShelter2 ash = new AnimalShelter2();
            ash.Enque(new Animal(Type.Dog, 1));
            ash.Enque(new Animal(Type.Cat, 2));
            ash.Enque(new Animal(Type.Cat, 3));
            ash.Enque(new Animal(Type.Dog, 4));
            ash.Enque(new Animal(Type.Cat, 5));

            Console.WriteLine(ash.DequeueAny());
            Console.WriteLine(ash.DequeCat());
            Console.WriteLine(ash.DequeDog());
            Console.WriteLine(ash.DequeCat());

            // Console.WriteLine(ash.DequeCat());

            Console.WriteLine(ash.DequeDog());
        }

        class AnimalShelter2
        {
            AnimalNode _head;
            AnimalNode _tail;

            public void Enque(Animal a)
            {
                if (_head is null)
                {
                    _head = new AnimalNode() { Animal = a };
                    _tail = _head;
                }
                else
                {
                    var n = new AnimalNode() { Animal = a };
                    _head.Next = n;
                    _head = n;
                }
            }

            public Animal DequeueAny()
            {
                if (_tail is null)
                    throw new Exception("emtpy");
                Animal a = _tail.Animal;
                _tail = _tail.Next;
                if (_tail is null)
                    _head = null;
                return a;
            }

            public Animal DequeCat()
            {
                return DequeueType(Type.Cat);
            }

            public Animal DequeDog()
            {
                return DequeueType(Type.Dog);
            }

            private Animal DequeueType(Type type)
            {
                if (_tail is null)
                    throw new Exception("empty");
                if (_tail.Animal.Type == type)
                    return DequeueAny();

                AnimalNode tmp = _tail;
                while (tmp.Next != null)
                {
                    if (tmp.Next.Animal.Type == type)
                    {
                        Animal a = tmp.Next.Animal;
                        tmp.Next = tmp.Next.Next;
                        return a;
                    }
                    else
                    {
                        tmp = tmp.Next;
                    }
                }
                throw new Exception("not found");
            }
        }

        class AnimalNode
        {
            public Animal Animal { get; set; }
            public AnimalNode Next { get; set; }
        }

        public static void AnimalShelterMy()
        {
            AnimalShelter ash = new AnimalShelter();
            ash.Enque(new Animal(Type.Dog, 1));
            ash.Enque(new Animal(Type.Cat, 2));
            ash.Enque(new Animal(Type.Cat, 3));
            ash.Enque(new Animal(Type.Dog, 4));
            ash.Enque(new Animal(Type.Cat, 5));

            Console.WriteLine(ash.DequeueAny());
            Console.WriteLine(ash.DequeCat());
            Console.WriteLine(ash.DequeDog());
            Console.WriteLine(ash.DequeCat());

            Console.WriteLine(ash.DequeDog());
        }

        class AnimalShelter
        {
            System.Collections.Generic.LinkedList<Animal> _animals = new System.Collections.Generic.LinkedList<Animal>();

            public void Enque(Animal a)
            {
                _animals.AddLast(a);
            }

            public Animal DequeueAny()
            {
                if (_animals.Count == 0)
                    throw new Exception("empty");

                LinkedListNode<Animal> last = _animals.First;
                _animals.RemoveFirst();
                return last.Value;
            }

            public Animal DequeCat()
            {
                return DequeueType(Type.Cat);
            }

            public Animal DequeDog()
            {
                return DequeueType(Type.Dog);
            }

            private Animal DequeueType(Type type)
            {
                if (_animals.Count == 0)
                    throw new Exception("empty");
                if (_animals.Last.Value.Type == type)
                    return DequeueAny();

                LinkedListNode<Animal> tmp = _animals.First;

                while (tmp.Next != null)
                {
                    if (tmp.Next.Value.Type == type)
                    {
                        LinkedListNode<Animal> a = tmp.Next;
                        _animals.Remove(tmp.Next);
                        return a.Value;
                    }
                    tmp = tmp.Next;
                }

                throw new Exception("Not found");
            }
        }

        [DebuggerDisplay("{Type}")]
        class Animal
        {
            public int Num { get; set; }
            public Animal(Type type, int num) => (Type, Num) = (type, num);
            public Type Type { get; set; }
            public override string ToString()
            {
                return $"{Type} {Num}";
            }
        }

        enum Type
        {
            Cat,
            Dog
        }

        public static void SortStackPg_238()
        {
            fxStack s1 = new fxStack();
            s1.Push(1);
            s1.Push(7);
            s1.Push(3);
            s1.Push(6);

            Sort(s1);

            while (s1.Count > 0)
            {
                Console.WriteLine(s1.Pop());
            }

            return;

            static void Sort(fxStack s1)
            {
                fxStack s2 = new fxStack();
                while (s1.Count > 0)
                {
                    int tmp = s1.Pop();
                    while (s2.Count > 0 && s2.Peek() > tmp)
                        s1.Push(s2.Pop());
                    s2.Push(tmp);
                }

                while (s2.Count > 0)
                {
                    s1.Push(s2.Pop());
                }
            }
        }

        public static void SortStackNoCount()
        {
            fxStack s1 = new fxStack();
            s1.Push(1);
            s1.Push(4);
            s1.Push(3);
            s1.Push(4);
            s1.Push(7);

            Sort(s1);

            while (s1.Count > 0)
            {
                Console.WriteLine(s1.Pop());
            }

            return;

            static void Sort(fxStack s1)
            {
                if (s1 is null || s1.Count == 0)
                    return;

                fxStack s2 = new fxStack();

                while (s1.Count > 0)
                {
                    int v = s1.Pop();
                    int n = 0;

                    while (s1.Count > 0)
                    {
                        int v2 = s1.Pop();
                        if (v2 <= v)
                        {
                            s2.Push(v);
                            v = v2;
                        }
                        else
                            s2.Push(v2);
                        n++;
                    }

                    while (n > 0)
                    {
                        s1.Push(s2.Pop());
                        n--;
                    }

                    s2.Push(v);
                }

                while (s2.Count > 0)
                    s1.Push(s2.Pop());
            }
        }

        public static void SortStack()
        {
            fxStack s1 = new fxStack();
            s1.Push(1);
            s1.Push(4);
            s1.Push(3);
            s1.Push(4);
            s1.Push(7);

            Sort(s1);

            while (s1.Count > 0)
            {
                Console.WriteLine(s1.Pop());
            }

            return;

            static void Sort(fxStack s1)
            {
                if (s1 is null || s1.Count == 0)
                    return;

                int n = 0;
                fxStack s2 = new fxStack();

                while (s1.Count > 0)
                {
                    s2.Push(s1.Pop());
                    n++;
                }

                while (s2.Count > 0)
                    s1.Push(s2.Pop());

                while (n > 0)
                {
                    int tmp = s1.Pop();
                    for (int i = n - 1; i > 0; i--)
                    {
                        int v = s1.Pop();
                        if (v > tmp)
                        {
                            s2.Push(tmp);
                            tmp = v;
                        }
                        else
                            s2.Push(v);
                    }

                    s1.Push(tmp);

                    while (s2.Count > 0)
                        s1.Push(s2.Pop());

                    n--;
                }

            }
        }

        public static void QueueViaStack()
        {
            // MyQueue mq = new MyQueue();
            MyQueue2 mq = new MyQueue2();

            mq.Add(1);
            mq.Add(2);
            mq.Add(3);

            Console.WriteLine(mq.Remove());
            Console.WriteLine(mq.Remove());
            Console.WriteLine(mq.Remove());
        }

        class MyQueue2
        {
            fxStack s1 = new fxStack();
            fxStack s2 = new fxStack();

            public void Add(int i)
            {
                s1.Push(i);
            }

            public int Remove()
            {
                if (s1.Count + s2.Count == 0)
                    throw new Exception("emtpy");

                if (s2.Count == 0)
                    while (s1.Count > 0)
                        s2.Push(s1.Pop());

                return s2.Pop();
            }
        }

        class MyQueue
        {
            fxStack s1 = new fxStack();
            fxStack s2 = new fxStack();
            int _c = 0;

            public void Add(int i)
            {
                s1.Push(i);
                _c++;
            }

            public int Remove()
            {
                if (s1.Count == 0)
                    throw new Exception("empty");

                int c = _c - 1;

                for (int i = c; c > 0; c--)
                {
                    s2.Push(s1.Pop());
                }

                int v = s1.Pop();

                while (s2.Count > 0)
                    s1.Push(s2.Pop());

                _c--;

                return v;
            }

        }

        public static void StackOfPlates()
        {
            StackOfPlatesType t = new StackOfPlatesType(3);

            // Console.WriteLine(t.Pop());

            t.Push(1);
            t.Push(2);
            t.Push(3);
            t.Push(4);
            Console.WriteLine("Peek " + t.Peek());
            t.Push(5);
            t.Push(6);

            while (!t.IsEmpty())
            {
                Console.WriteLine(t.Pop());
            }

            Console.WriteLine();
            t.Push(1);
            t.Push(2);
            t.Push(3);
            t.Push(4);
            t.Push(5);
            t.Push(6);

            Console.WriteLine("Pop at " + t.PopAt(4));
            Console.WriteLine();

            while (!t.IsEmpty())
            {
                Console.WriteLine(t.Pop());
            }

            Console.WriteLine();
            t.Push(1);
            t.Push(2);
            t.Push(3);
            while (!t.IsEmpty())
            {
                Console.WriteLine(t.Pop());
            }
        }

        class StackOfPlatesType
        {
            List<int[]> _arrList = new List<int[]>();
            int _index = -1;
            int _capacity = 0;

            public StackOfPlatesType(int capacity)
            {
                _capacity = capacity;
            }

            public int GetArrayIndex(int index)
            {
                return index / _capacity;
            }

            public int GetIndexIntoArray(int index)
            {
                return index % _capacity;
            }

            public void Push(int val)
            {
                _index++;
                int arr = GetArrayIndex(_index);
                if (arr + 1 > _arrList.Count)
                    _arrList.Add(new int[_capacity]);

                _arrList[arr][GetIndexIntoArray(_index)] = val;
            }

            public int PopAt(int index)
            {
                if (_index == -1)
                    throw new Exception("empty");

                var v = _arrList[GetArrayIndex(index)][GetIndexIntoArray(index)];

                ShiftLeft(index);

                _index--;

                return v;
            }

            public void ShiftLeft(int index)
            {
                while (index < _index)
                {
                    _arrList[GetArrayIndex(index)][GetIndexIntoArray(index)] =
                        _arrList[GetArrayIndex(index + 1)][GetIndexIntoArray(index + 1)];
                    index++;
                }
            }

            public int Pop()
            {
                return PopAt(_index);
            }

            public bool IsEmpty() => _index == -1;

            public int Peek()
            {
                if (_index == -1)
                    throw new Exception("empty");

                return _arrList[GetArrayIndex(_index)][GetIndexIntoArray(_index)];
            }
        }

        public static void QueueNodes()
        {
            // QueueNodesType t = new QueueNodesType();
            QueueFirstLast t = new QueueFirstLast();

            t.Enqueue(1);
            t.Enqueue(2);
            t.Enqueue(3);
            t.Enqueue(4);

            Console.WriteLine(t.Dequeue());
            Console.WriteLine(t.Dequeue());
            Console.WriteLine(t.Dequeue());
            Console.WriteLine(t.Dequeue());
            Console.WriteLine(t.Dequeue());
        }

        class QueueFirstLast
        {
            Node _first; //first to exit
            Node _last; // last inserted

            public void Enqueue(int v)
            {
                Node n = new Node(v);

                if (_last != null)
                {
                    _last.Next = n; // append old last to new last
                }

                _last = n; // set new enqued node as last

                if (_first is null) // set first if null
                {
                    _first = _last;
                }
            }

            public int Dequeue()
            {
                if (_first is null)
                    throw new Exception("empty");

                int val = _first.Val;
                _first = _first.Next;

                if (_first is null)
                {
                    _last = null;
                }

                return val;
            }

            class Node
            {
                public Node(int val) => Val = val;
                public int Val { get; set; }
                public Node Next { get; set; }
            }
        }

        class QueueNodesType
        {
            Node _head;
            Node _tail;

            public void Enqueue(int v)
            {
                Node n = new Node(v);
                if (_head is null)
                {
                    _tail = _head = n;
                }
                else
                {
                    _head = _head.Next = n;
                }
            }

            public int Dequeue()
            {
                if (_tail is null)
                    throw new Exception("empty");

                int val = _tail.Val;
                _tail = _tail.Next;

                if (_tail is null)
                    _head = null;

                return val;
            }

            class Node
            {
                public Node(int val) => Val = val;
                public int Val { get; set; }
                public Node Next { get; set; }
            }
        }

        public static void StackWithArray()
        {
            StackArray sa = new StackArray();
            sa.Push(1);
            sa.Push(2);
            sa.Push(3);
            sa.Push(4);
            Console.WriteLine(sa.Count());
            Console.WriteLine();
            while (!sa.IsEmpty())
            {
                Console.WriteLine(sa.Pop());
            }
        }

        class StackArray
        {
            int[] array = new int[100];
            int _currentIndex = -1;

            public void Push(int v)
            {
                array[++_currentIndex] = v;
            }

            public int Pop()
            {
                if (_currentIndex == -1)
                    throw new Exception("Empty");

                return array[_currentIndex--];
            }

            public bool IsEmpty() => _currentIndex == -1;

            public int Peek()
            {
                if (_currentIndex == -1)
                    throw new Exception("Empty");

                return array[_currentIndex];
            }

            public int Count()
            {
                if (_currentIndex == -1)
                    throw new Exception("Empty");

                return _currentIndex + 1;
            }
        }

        public static void StackMin_Pg99()
        {
            StackMin2 sm = new StackMin2();
            sm.Push(1);
            sm.Push(3);
            sm.Push(2);
            sm.Push(0);

            Console.WriteLine(sm.Min());
            Console.WriteLine(sm.Pop());
            Console.WriteLine(sm.Min());
        }

        class StackMin2
        {
            Node2 _head;
            Node2 _min;

            public int Pop()
            {
                if (_head is null)
                    throw new Exception("Empty");

                int val = _head.Value;

                if (val == _min.Value)
                {
                    _min = _min.Next;
                }

                _head = _head.Next;
                return val;
            }

            public int Min()
            {
                if (_min is null)
                    throw new Exception("Empty");

                return _min.Value;
            }

            public void Push(int value)
            {
                if (_min is null)
                {
                    _min = new Node2(value);
                }
                else
                {
                    if (value <= _min.Value)
                    {
                        var newMin = new Node2(value);
                        newMin.Next = _min;
                        _min = newMin;
                    }
                }

                Node2 newNode = new Node2(value);
                newNode.Next = _head;
                _head = newNode;
            }

            class Node2
            {
                public Node2(int value) => (Value) = (value);
                public int Value { get; set; }
                public Node2 Next { get; set; }
            }
        }

        public static void StackMin_Pg98()
        {
            StackMin sm = new StackMin();
            sm.Push(1);
            sm.Push(3);
            sm.Push(2);
            sm.Push(0);

            Console.WriteLine(sm.Min());
            Console.WriteLine(sm.Pop());
            Console.WriteLine(sm.Min());

        }

        class StackMin
        {
            Node _head;

            public void Print()
            {
                Node r = _head;
                while (r != null)
                {
                    Console.WriteLine(r.Value);
                    r = r.Next;
                }
                Console.WriteLine("end stack");
                Console.WriteLine();
            }

            public int Pop()
            {
                if (_head is null)
                    throw new Exception("Empty");

                int val = _head.Value;
                _head = _head.Next;
                return val;
            }

            public int Min()
            {
                if (_head is null)
                    throw new Exception("Empty");

                return _head.Min;
            }

            public void Push(int value)
            {
                Node newNode = new Node(value, Math.Min(value, _head.Min));
                newNode.Next = _head;
                _head = newNode;
            }

            class Node
            {
                public int Min { get; set; }
                public Node(int value, int min) => (Value, Min) = (value, min);

                public int Value { get; set; }
                public Node Next { get; set; }
            }
        }

        public static void TreeStack()
        {
            int[] array = new int[100];

            ThreeStack<int> first = new ThreeStack<int>(0, 3, array);
            ThreeStack<int> second = new ThreeStack<int>(1, 3, array);
            ThreeStack<int> third = new ThreeStack<int>(2, 3, array);

            first.Push(1);
            first.Push(2);
            first.Push(3);

            Drain(first);

            second.Push(4);
            second.Push(5);
            second.Push(6);

            Drain(second);

            third.Push(7);
            third.Push(8);
            third.Push(9);

            Drain(third);

            first.Push(1);
            first.Push(2);
            first.Push(3);

            Drain(first);

            static void Drain(ThreeStack<int> stack)
            {
                while (!stack.IsEmpty())
                {
                    Console.WriteLine(stack.Pop());
                }
                Console.WriteLine();
            }

        }

        class ThreeStack<T>
        {
            int _count;
            int _currentIndex;
            int _numOfStacks;
            T[] _array;

            public delegate void ExpandDelegate();
            public event ExpandDelegate Expand;

            public ThreeStack(int currentIndex, int numOfStacks, T[] array)
            {
                _numOfStacks = numOfStacks;
                _currentIndex = currentIndex;
                _array = array;
            }

            public void Push(T item)
            {
                if (_count > 0)
                {
                    _currentIndex += _numOfStacks;
                }

                if (_currentIndex > _array.Length - 1)
                    RaiseExpand();

                _array[_currentIndex] = item;
                _count++;
            }

            public T Pop()
            {
                if (_count == 0)
                    return default;

                T item = _array[_currentIndex];
                _count--;

                if (_count > 0)
                    _currentIndex -= _numOfStacks;

                return item;
            }

            public T Peek()
            {
                if (_count == 0)
                    return default;

                return _array[_currentIndex];
            }

            public bool IsEmpty()
            {
                return _count == 0;
            }

            private void RaiseExpand() { this?.Expand(); }
        }
    }
}
