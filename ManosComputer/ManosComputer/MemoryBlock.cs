using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManosComputer
{
    public class MemoryBlock
    {
        public byte[] Data { get; protected set; }

        public MemoryBlock()
        {
            Data = new byte[Program.WORD_SIZE];
        }

        // Sets the Data array to the correct bit based on the value passed in, with respect to the lower and upper bounds given.
        public void SetBitsFromValueWithBound(int value, int lowBit, int highBit)
        {
            var divisor = value;

            for (var i = 0; i < (highBit - lowBit) + 1; i++)
            {
                Data[lowBit + i] = (byte)(divisor % 2);
                divisor /= 2;
            }
        }

        // Property for the decimal value stored in this memory block.
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

        // Property for the binary value as a string stored in this memory block.
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

        // Property for the hex value as a string stored in this memory block.
        public string HexValue
        {
            get
            {
                return DecValue.ToString("X").PadLeft(4, '0');
            }
        }
    }
}
