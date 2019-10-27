using System;
using System.Collections.Generic;
using System.Text;

namespace Exp1
{
    public class DoubleNegation
    {
        public static float M1(float a)
        {
            return -(-a); // e.g. after inlining
        }

        public static int I1(int a)
        {
            return -(-a);
        }
    }
}
