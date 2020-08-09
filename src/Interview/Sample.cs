using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Interview
{
    [DebuggerDisplay("{Name}")]
    public class Project
    {
        public List<Project> DependsOn { get; set; } = new List<Project>();
        public List<Project> IsDependencyFor { get; set; } = new List<Project>();
        public string Name { get; set; }
        public void AddDependand(Project p)
        {
            if (!DependsOn.Contains(p))
            {
                DependsOn.Add(p);
            }
            if (!p.IsDependencyFor.Contains(this))
            {
                p.IsDependencyFor.Add(this);
            }
        }
    }

    public class Sample
    {
        public void Start()
        {
            // Stack();
            BuildOrder();
        }

        public void BuildOrder()
        {
            Project a = new Project() { Name = "A" };
            Project b = new Project() { Name = "B" };
            Project c = new Project() { Name = "C" };
            Project d = new Project() { Name = "D" };
            Project e = new Project() { Name = "E" };
            Project f = new Project() { Name = "F" };

            a.AddDependand(b);

            b.AddDependand(c);
            b.AddDependand(d);

            c.AddDependand(f);

            d.AddDependand(e);

            // Ciruclar Dep
            // e.AddDependand(a);
            // f.AddDependand(a);
            // f.AddDependand(b);



            List<Project> pjs = new List<Project>() { a, b, c, d, e, f };
            int prjN = pjs.Count;
            List<Project> finalOrder = new List<Project>();

            int totalProject = pjs.Count;
            while (totalProject > 0)
            {
                bool circularDep = true;

                foreach (Project p in pjs.ToArray())
                {
                    if (p.DependsOn.Count == 0)
                    {
                        finalOrder.Add(p);
                        pjs.Remove(p);
                        totalProject--;
                        circularDep = false;

                        foreach (Project p2 in p.IsDependencyFor)
                        {
                            p2.DependsOn.Remove(p);
                        }
                    }
                }

                if (circularDep)
                {
                    throw new Exception("Circular dep");
                }
            }

            foreach (var item in finalOrder)
            {
                Console.Write(item.Name + " ");
            }
            Debug.Assert(finalOrder.Count == prjN);
        }

        public void Stack()
        {
            System.Collections.Generic.Stack<int> s = new System.Collections.Generic.Stack<int>();

            System.Collections.Generic.Stack<int> s2 = new System.Collections.Generic.Stack<int>();
            s.Push(1);
            s.Push(2);
            s.Push(3);

            while (s.Count > 0)
            {
                Console.WriteLine(s.Pop());
            }

            s.Push(1);
            s.Push(2);
            s.Push(3);

            int c = s.Count;

            while (--c > 0)
            {
                int i = s.Pop();

                int toMove = c;
                while (toMove > 0)
                {
                    s2.Push(s.Pop());
                    toMove--;
                }

                s.Push(i);

                while (s2.Count > 0)
                    s.Push(s2.Pop());
            }

            Console.WriteLine();

            while (s.Count > 0)
            {
                Console.WriteLine(s.Pop());
            }

        }
    }
}
