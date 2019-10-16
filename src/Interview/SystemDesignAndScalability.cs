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
            string _name;
            public string VisitedBy { get; set; }
            public string FromPath { get; set; }

            public Person(string name) => _name = name;
            public List<Person> Friends { get; set; } = new List<Person>();

            public string ShortestPathParallel(Person b)
            {
                Queue<Person> qa = new Queue<Person>();
                qa.Add(this);

                Queue<Person> qb = new Queue<Person>();
                qb.Add(b);

                HashSet<Person> visited = new HashSet<Person>();

                while (!qa.IsEmpty() && !qb.IsEmpty())
                {
                    Person va = qa.Remove();
                    va.VisitedBy = "Left";
                    visited.Add(va);

                    Person vb = qb.Remove();
                    vb.VisitedBy = "Right";
                    visited.Add(vb);

                    foreach (var p in va.Friends)
                    {
                        if (!visited.Contains(p))
                        {
                            p.FromPath += va.FromPath + " " + va._name;
                            qa.Add(p);
                        }
                        else
                        {
                            if (visited.TryGetValue(p, out Person actual) && actual.VisitedBy == "Right")
                            {

                            }
                        }
                    }

                    foreach (var p in vb.Friends)
                    {
                        if (!visited.Contains(p))
                        {
                            p.FromPath += va.FromPath + " " + va._name;
                            qb.Add(p);
                        }
                        else
                        {
                            if (visited.TryGetValue(p, out Person actual) && actual.VisitedBy == "Left")
                            {

                            }
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
                        return va.FromPath + " " + b._name;

                    foreach (var p in va.Friends)
                    {
                        if (!visitedA.Contains(p))
                        {
                            p.FromPath += va.FromPath + " " + va._name;
                            qa.Add(p);
                        }
                    }
                }

                return "";
            }
        }
    }
}
