using System;
using System.Collections;

namespace IT_Lab2_Encryptor
{
    class Encryptor
    {
        private int[] _toXor =
        {
            33, 13
        };

        public int[] ToXor => _toXor;

        public BitArray Encrypt(BitArray key, BitArray data)
        {
            Console.WriteLine("Key: {0}", key.ToBitString());
            BitArray generatedKey = GenerateKey(key, data.Count);
            Console.WriteLine("Generated key: {0}", generatedKey.ToBitString());
            return data.Xor(generatedKey);
        }

        private BitArray GenerateKey(BitArray key, int requiredLen)
        {
            LSFR register = new LSFR(key, ToXor);
            BitArray generatedKey = new BitArray(requiredLen);
            for (int i = requiredLen - 1; i > 0; i--)
            {
                generatedKey[i] = register.GetNext();
            }

            return generatedKey;
        }
    }

    class LSFR
    {
        private int[] _toXor;

        public int[] ToXor
        {
            get => _toXor;
            set => _toXor = value;
        }

        private BitArray _data;

        public BitArray Data
        {
            get => _data;
            private set => _data = value;
        }

        public LSFR(BitArray initializeData, int[] toXor)
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
           // Console.WriteLine("Before SHift: {0}", Data.ToBitString());
            Data = Data.LeftShift(1);
            Data[0] = lastElement;
            //Console.WriteLine("After Shift: {0}", Data.ToBitString());
        }

        public bool GetNext()
        {
            bool result = Data[Data.Count - 1];
            Iterate();
            return result;
        }
    }
}