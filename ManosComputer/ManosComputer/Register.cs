using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManosComputer
{
    // Represents different sizes that the registers can have.
    public enum RegisterSize
    {
        SixteenBit = Program.WORD_SIZE,
        TwelveBit  = (Program.WORD_SIZE / 4) * 3,
        OneBit     = 1
    }

    public class Register : MemoryBlock
    {
        public string Abbr { get; private set; }

        public Register(RegisterSize size, string abbr)
        {
            Data = new byte[(int)size];
            Abbr = abbr;
        }
    }
}
