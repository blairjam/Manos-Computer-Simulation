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

    // Bit flags for the possible flags each register can have.
    [Flags]
    public enum RegisterFlags
    {
        None      = 0b000,
        Load      = 0b001,
        Increment = 0b010,
        Clear     = 0b100,
        All       = Load | Increment | Clear
    }

    public class Register : MemoryBlock
    {
        private RegisterFlags flags;

        private bool loadFlag = false;
        private bool incrementFlag = false;
        private bool clearFlag = false;

        public Register(RegisterSize size, RegisterFlags flags)
        {
            Data = new byte[(int)size];
            this.flags = flags;
        }

        public void SetBits(int value)
        {
            SetBitsFromValue(value, 0x0, Data.Length - 1);
        }

        public bool LoadFlag
        {
            get
            {
                return loadFlag;
            }

            set
            {
                if ((flags & RegisterFlags.Load) == RegisterFlags.Load)
                {
                    loadFlag = value;
                }
            }
        }

        public bool IncrementFlag
        {
            get
            {
                return incrementFlag;
            }

            set
            {
                if ((flags & RegisterFlags.Increment) == RegisterFlags.Increment)
                {
                    incrementFlag = value;
                }
            }
        }

        public bool ClearFlag
        {
            get
            {
                return clearFlag;
            }

            set
            {
                if ((flags & RegisterFlags.Clear) == RegisterFlags.Clear)
                {
                    clearFlag = value;
                }
            }
        }

        public void ResetFlags()
        {
            loadFlag = false;
            incrementFlag = false;
            clearFlag = false;
        }
    }
}
