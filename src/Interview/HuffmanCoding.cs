using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Interview
{
    public static class HuffmanCoding
    {
        public static void Test()
        {
            string text = "My names is Marco Rossignoli and I live in Italy";
            Console.WriteLine($"To encode {text} initial len {text.Length}");
            Dictionary<char, int> frequencyTable = new Dictionary<char, int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (!frequencyTable.ContainsKey(text[i]))
                {
                    frequencyTable.Add(text[i], 0);
                }
                frequencyTable[text[i]]++;
            }

            FrequencyNode[] frequencyTableList = frequencyTable.OrderBy(v => v.Value).Select(k => new FrequencyNode() { FrequencyValue = k.Value, Char = k.Key }).ToArray();
            FrequencyNode[] ft = frequencyTableList.ToArray();

            foreach (var v in ft)
            {
                Console.WriteLine($"{v.Char} {v.FrequencyValue}");
            }

            while (ft.Length > 1)
            {
                FrequencyNode node = new FrequencyNode()
                {
                    FrequencyValue = ft[0].FrequencyValue + ft[1].FrequencyValue
                };

                if (ft[0].FrequencyValue < ft[1].FrequencyValue)
                {
                    node.Left = ft[0];
                    node.Right = ft[1];
                }
                else
                {
                    node.Left = ft[1];
                    node.Right = ft[0];
                }

                var tmp = new List<FrequencyNode>(ft.Skip(2).ToArray());
                tmp.Add(node);

                ft = tmp.OrderBy(n => n.FrequencyValue).ToArray();
            }

            Console.WriteLine("-------");

            AssignCode(ft[0], "");

            Dictionary<char, string> encodes = new Dictionary<char, string>();

            foreach (FrequencyNode val in frequencyTableList.OrderByDescending(f => f.FrequencyValue))
            {
                Console.WriteLine($"{val.Char} {val.FrequencyValue} {val.StringCode}");
                encodes.Add(val.Char.Value, val.StringCode);
            }

            StringBuilder encoded = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                encoded.Append(encodes[text[i]]);
            }

            string encodedVal = encoded.ToString();
            Console.WriteLine($"Encoded {encoded} len {encodedVal.Length / 8}");

            int encodedLen = encodedVal.Length;
            int currentIndex = 0;
            while (currentIndex < encodedLen)
            {
                currentIndex = Decoding(ft[0], encodedVal, currentIndex);
            }

            return;

            static int Decoding(FrequencyNode root, string encodedValue, int index)
            {
                if (root.IsLeaf)
                {
                    Console.Write(root.Char);
                    return index;
                }

                if (encodedValue[index] == '0')
                {
                    return Decoding(root.Left, encodedValue, index + 1);
                }
                else
                {
                    return Decoding(root.Right, encodedValue, index + 1);
                }
            }

            static void AssignCode(FrequencyNode root, string prefix)
            {
                if (root.IsLeaf)
                {
                    root.StringCode = prefix;
                    return;
                }

                if (root.Left != null)
                {
                    AssignCode(root.Left, prefix + "0");
                }

                if (root.Right != null)
                {
                    AssignCode(root.Right, prefix + "1");
                }
            }
        }
    }


    [DebuggerDisplay("FrequencyValue = {FrequencyValue} Char = {Char} StringCode = {StringCode}")]
    public class FrequencyNode
    {
        public bool IsLeaf { get { return Char.HasValue; } }
        public int FrequencyValue { get; set; }
        public char? Char { get; set; }
        public FrequencyNode Left { get; set; }
        public FrequencyNode Right { get; set; }
        public string StringCode { get; set; }
    }
}
