using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dic
{
    partial class Assert
    {
        static IComparer<T> GetComparer<T>() where T : IComparable
        {
            return new AssertComparer<T>();
        }

        static IEqualityComparer<T> GetEqualityComparer<T>(IEqualityComparer innerComparer = null)
        {
            return new AssertEqualityComparer<T>(innerComparer);
        }

        public static void Equal<T>(T expected, T actual)
        {
            Equal(expected, actual, GetEqualityComparer<T>());
        }

        public static void Equal<T>(T expected, T actual, IEqualityComparer<T> comparer)
        {
            if (!comparer.Equals(expected, actual))
                // throw new EqualException(expected, actual);
                throw new Exception("Not equal");
        }
    }
}
