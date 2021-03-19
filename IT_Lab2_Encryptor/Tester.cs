using System;
using System.Collections;

namespace IT_Lab2_Encryptor
{
    public class Tester
    {
        public static void Main(string[] args)
        {
            string name = "vladik";
            byte[] b = {1};
            BitArray a = new BitArray(b);
            Console.WriteLine(a.ToBitString());
            BitArray key = new BitArray(new bool[]
            {
                true, false, true, true, true, false, true, false,
                true, false, true, false, true, false, true, false,
                true, true, true, false, true, false, true, false,
                true, false, true, false, true, false, true, false, true
            });
            Encryptor encryptor = new Encryptor();
            BitArray data = new BitArray(System.Text.Encoding.Unicode.GetBytes(name));
            Console.WriteLine("Input data: {0}", data.ToBitString());
            BitArray result = encryptor.Encrypt(key, data);
            Console.WriteLine("Cipher: {0}", result.ToBitString());
            Console.WriteLine(System.Text.Encoding.Unicode.GetString(result.ToByteArray()));
            result = encryptor.Encrypt(key, result);
            Console.WriteLine("Plain: {0}", result.ToBitString());
            Console.WriteLine(System.Text.Encoding.Unicode.GetString(result.ToByteArray()));
        }
    }
}