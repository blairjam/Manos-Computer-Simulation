using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManosComputer
{
    public enum RegisterSize
    {
        SixteenBit = Program.WORD_SIZE,
        TwelveBit = (Program.WORD_SIZE / 4) * 3,
        OneBit = 1
    }

    [Flags]
    public enum RegisterFlags
    {
        None = 0b0,
        Load = 0b1,
        Increment = 0b10,
        Clear = 0b100,
        All = Load | Increment | Clear
    }

    public class Register
    {
        public byte[] Bits { get; protected set; }
        private RegisterFlags flags;

        private bool loadFlag = false;
        private bool incrementFlag = false;
        private bool clearFlag = false;

        public Register(RegisterSize size, RegisterFlags flags)
        {
            Bits = new byte[(int)size];
            this.flags = flags;
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
