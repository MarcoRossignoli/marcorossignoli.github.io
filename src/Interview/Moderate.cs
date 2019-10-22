using System;
using fxStack = System.Collections.Generic.Stack<string>;

namespace Interview
{
    class Moderate
    {
        public static void Calculator_Pg185()
        {
            string expression = "2*3+5/6*3+15";

            fxStack stack = new fxStack();

            stack.Push("15");
            stack.Push("+");
            stack.Push("3");
            stack.Push("*");
            stack.Push("6");
            stack.Push("/");
            stack.Push("5");
            stack.Push("+");
            stack.Push("3");
            stack.Push("*");
            stack.Push("2");

            while (stack.Count > 1)
            {
                double opA = double.Parse(stack.Pop());
                string op = stack.Pop();
                double opB = double.Parse(stack.Pop());

                if (op == "*" || op == "/")
                {
                    stack.Push((op == "*" ? opA * opB : opA / opB).ToString());
                }
                else
                {
                    string nextOp = "";
                    if (stack.Count > 0)
                    {
                        nextOp = stack.Peek();
                    }
                    if (nextOp == "*" || nextOp == "/")
                    {
                        string nextOpToExecute = stack.Pop();
                        double opC = double.Parse(stack.Pop());
                        stack.Push((nextOpToExecute == "*" ? opB * opC : opB / opC).ToString());
                        stack.Push(op);
                        stack.Push(opA.ToString());
                    }
                    else
                    {
                        if (op == "+")
                            stack.Push((opA + opB).ToString());
                        if (op == "-")
                            stack.Push((opA - opB).ToString());
                    }
                }
            }
            Console.WriteLine(stack.Pop());
        }
    }
}
