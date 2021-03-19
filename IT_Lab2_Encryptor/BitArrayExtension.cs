using System;
using System.Collections;
using System.Text;

namespace IT_Lab2_Encryptor
{
    public static class BitArrayExtension
    {
        public static byte[] ToByteArray(this BitArray bits) 
        {
            /*int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++) 
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8) {
                    bitIndex = 0;
                    byteIndex++;
                }
            }*/

            byte[] bytes = new byte[(int) Math.Ceiling(bits.Count / 8.0)];
            bits.CopyTo(bytes, 0);
            return bytes;
        }
        
        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = bits.Count - 1; i > 0; i--)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}