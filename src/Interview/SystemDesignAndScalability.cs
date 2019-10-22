using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Interview
{
    class SystemDesignAndScalability
    {
        public static void SocialNetwork()
        {
            Person a = new Person("A");
            Person b = new Person("B");
            Person c = new Person("C");
            Person d = new Person("D");
            Person e = new Person("E");
            Person f = new Person("F");
            Person g = new Person("G");

            a.Friends.Add(b);
            b.Friends.Add(a);

            a.Friends.Add(c);
            c.Friends.Add(a);

            c.Friends.Add(g);
            g.Friends.Add(c);

            f.Friends.Add(e);
            e.Friends.Add(f);

            f.Friends.Add(d);
            d.Friends.Add(f);

            e.Friends.Add(g);
            g.Friends.Add(e);


            // Console.WriteLine(a.ShortestPath(f));
            Console.WriteLine(a.ShortestPathParallel(f));
            Console.WriteLine("End");
        }

        [DebuggerDisplay("{_name}")]
        class Person
        {
            public enum VisitState
            {
                Unvisited,
                InVisiting,
                Visited,
            }

            string _name;

            public string PastNodes { get; set; }

            public VisitState VisitStateValue { get; set; } = Person.VisitState.Unvisited;

            public Person(string name) => _name = name;
            public List<Person> Friends { get; set; } = new List<Person>();

            public string ShortestPathParallel(Person b)
            {
                Queue<Person> qa = new Queue<Person>();
                this.PastNodes = this._name;
                qa.Add(this);

                Queue<Person> qb = new Queue<Person>();
                b.PastNodes = b._name;
                qb.Add(b);

                HashSet<Person> visited = new HashSet<Person>();

                while (!qa.IsEmpty() && !qb.IsEmpty())
                {
                    Person va = qa.Remove();
                    if (!visited.Contains(va))
                    {
                        va.VisitStateValue = VisitState.Visited;
                        visited.Add(va);
                    }

                    Person vb = qb.Remove();
                    if (!visited.Contains(vb))
                    {
                        vb.VisitStateValue = VisitState.Visited;
                        visited.Add(vb);
                    }

                    foreach (var p in va.Friends)
                    {
                        if (!visited.Contains(p))
                        {
                            if (p.VisitStateValue == VisitState.Unvisited)
                            {
                                p.PastNodes += p.PastNodes + va._name;
                                p.VisitStateValue = VisitState.InVisiting;
                            }
                            else
                            {
                                return va.PastNodes + va._name + p._name + p.PastNodes + b._name;
                            }
                            qa.Add(p);
                        }
                    }

                    foreach (var p in vb.Friends)
                    {
                        if (!visited.Contains(p))
                        {
                            if (p.VisitStateValue == VisitState.Unvisited)
                            {
                                p.PastNodes += p.PastNodes + vb._name;
                                p.VisitStateValue = VisitState.InVisiting;
                            }
                            else
                            {
                                return vb.PastNodes + vb._name + p._name + p.PastNodes + this._name;
                            }
                            qb.Add(p);
                        }
                    }
                }

                return "";
            }

            public string ShortestPath(Person b)
            {
                Queue<Person> qa = new Queue<Person>();

                HashSet<Person> visitedA = new HashSet<Person>();

                qa.Add(this);

                while (!qa.IsEmpty())
                {
                    Person va = qa.Remove();
                    visitedA.Add(va);

                    if (va == b)
                        return va.PastNodes + " " + b._name;

                    foreach (var p in va.Friends)
                    {
                        if (!visitedA.Contains(p))
                        {
                            p.PastNodes += va.PastNodes + " " + va._name;
                            qa.Add(p);
                        }
                    }
                }

                return "";
            }
        }
    }
}
