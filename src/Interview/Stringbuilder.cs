using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Interview.ArrayAndString
{
    public class ArrayAndString_StringBuilder
    {
        public static void TestStringbuilder()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("AAAA");
            builder.Append("BBBBB");
            builder.Append("CCCCCCC");
            Debug.Assert("AAAABBBBBCCCCCCC" == builder.ToString());
        }
    }

    public class StringBuilder
    {
        private string[] _fragments = new string[2];
        private int _size = 0;
        private int _finalStringSize = 0;

        public void Append(string str)
        {
            if (_fragments.Length < (_size + 1))
            {
                EnsureCapacity(_size + 1);
            }
            _fragments[_size] = str;
            _size++;
            _finalStringSize += str.Length;
        }

        private void EnsureCapacity(int size)
        {
            var tmp = _fragments;
            _fragments = new string[tmp.Length * 2];
            Array.Copy(tmp, _fragments, tmp.Length);
        }

        public override string ToString()
        {
            char[] finalString = new char[_finalStringSize];

            int currentIndex = 0;
            for (int i = 0; i < _size; i++)
            {
                Array.Copy(_fragments[i].ToCharArray(), 0, finalString, currentIndex, _fragments[i].Length);
                currentIndex += _fragments[i].Length;
            }

            return new string(finalString);
        }
    }
}
