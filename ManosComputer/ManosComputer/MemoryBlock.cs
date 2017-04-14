using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManosComputer
{
    public class MemoryBlock
    {
        public MemoryBlock()
        {
            Data = new byte[Program.WORD_SIZE];
        }

        public byte[] Data
        {
            get; private set;
        }

        public void SetBits(int value, int lowBit, int highBit)
        {
            var divisor = value;

            for (var i = 0; i < (highBit - lowBit) + 1; i++)
            {
                Data[lowBit + i] = (byte)(divisor % 2);
                divisor /= 2;
            }
        }

        public int DecValue
        {
            get
            {
                var value = 0;

                for (var i = Data.Length - 1; i >= 0; i--)
                {
                    value <<= 1;

                    if (Data[i] != 0)
                    {
                        value++;
                    }
                }

                return value;
            }
        }

        public string BinValue
        {
            get
            {
                var value = "(";

                for (var i = Data.Length - 1; i >= 0; i--)
                {
                    value += Data[i];

                    if (i % 4 == 0 && i > 0)
                    {
                        value += " ";
                    }
                }

                value += ")";

                return value;
            }
        }

        public string HexValue
        {
            get
            {
                return DecValue.ToString("X").PadLeft(4, '0');
            }
        }
    }
}
