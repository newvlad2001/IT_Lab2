using System;
using System.Collections;
using System.Text;

namespace IT_Lab2_Encryptor
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this BitArray bits)
        {

            byte[] bytes = new byte[(int) Math.Ceiling(bits.Count / 8.0)];
            bits.CopyTo(bytes, 0);
            return bytes;
        }

        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = bits.Count - 1; i >= 0; i--)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }


            return sb.ToString();
        }

        public static string ToBitStringByBytes(this BitArray bits)
        {
            StringBuilder tmp = new StringBuilder(bits.ToBitString());
            StringBuilder result = new StringBuilder();
            if (bits.Count % 8 != 0)
            {
                for (int i = 0; i < 8 - bits.Count % 8; i++)
                {
                    result.Append('0');
                }
            }

            result.Append(tmp);
            int counter = 0;
            for (int i = result.Length - 1; i >= 0; i--, counter++)
            {
                if (counter == 8)
                {
                    result.Insert(i + 1, ' ');
                    counter = 0;
                }
            }

            return result.ToString();
        }

        public static BitArray StringToBitArray(this string str)
        {
            BitArray array = new BitArray(str.Length);
            for (int i = 0; i < str.Length; i++)
            {
                bool bit = str[i].Equals('1');
                array[str.Length - 1 - i] = bit;
            }

            return array;
        }
    }
}