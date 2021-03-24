using System;
using System.Collections;

namespace IT_Lab2_Encryptor
{
    public class Encryptor
    {
        public static int[] ToXor { get; } =
        {
            33, 13
        };


        public BitArray GeneratedKey { get; private set; }

        public BitArray Encrypt(BitArray key, BitArray data)
        {
            Console.WriteLine("Key: {0}", key.ToBitString());
            GeneratedKey = GenerateKey(key, data.Count);
            //Console.WriteLine("Generated key: {0}", GeneratedKey.ToBitString());
            return data.Xor(GeneratedKey);
        }

        private BitArray GenerateKey(BitArray key, int requiredLen)
        {
            LFSR register = new LFSR(key, ToXor);
            BitArray generatedKey = new BitArray(requiredLen);
            for (int i = requiredLen - 1; i >= 0; i--)
            {
                generatedKey[i] = register.GetNext();
            }

            Console.WriteLine("Amount of iterations: {0}", register.Counter);
            return generatedKey;
        }
    }

    class LFSR
    {
        private int[] _toXor;
        public int Counter { get; private set; }

        public int[] ToXor
        {
            get => _toXor;
            set => _toXor = value;
        }

        private BitArray _data;

        private BitArray Data
        {
            get => _data;
            set => _data = value;
        }

        public LFSR(BitArray initializeData, int[] toXor)
        {
            ToXor = toXor;
            Data = new BitArray(initializeData);
        }

        private void Iterate()
        {
            bool lastElement = Data[ToXor[0] - 1];
            for (int i = 1; i < ToXor.Length; i++)
            {
                lastElement ^= Data[ToXor[i] - 1];
            }

            //Console.WriteLine(Data.ToBitString());
            Data.LeftShift(1);
            Data[0] = lastElement;
            //Console.WriteLine("After Shift: {0}", Data.ToBitString());
        }

        public bool GetNext()
        {
            Counter++;
            bool result = Data[Data.Count - 1];
            Iterate();
            return result;
        }
    }
}