using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManosComputer
{
    public class Program
    {
        public const int WORD_SIZE = 16;

        private const int MEM_SIZE  = 4096;

        private const int ADDRESS_REGISTER     = 0;
        private const int PROGRAM_CONTROL      = 1;
        private const int DATA_REGISTER        = 2;
        private const int ACCUMULATOR          = 3;
        private const int INSTRUCTION_REGISTER = 4;
        private const int TEMPORARY_REGISTER   = 5;
        private const int E_REGISTER           = 6;

        private static readonly int[] REG_REF_INSTR_NUMS = { 0x800, 0x400, 0x200, 0x100,
                                                             0x080, 0x040, 0x020, 0x010,
                                                             0x008, 0x004, 0x002, 0x001 };

        private List<Register> registers = new List<Register>();
        private List<MemoryBlock> memory = new List<MemoryBlock>();

        public Program()
        {
            registers.Add(new Register(RegisterSize.TwelveBit, RegisterFlags.All));
            registers.Add(new Register(RegisterSize.TwelveBit, RegisterFlags.All));
            registers.Add(new Register(RegisterSize.SixteenBit, RegisterFlags.All));
            registers.Add(new Register(RegisterSize.SixteenBit, RegisterFlags.All));
            registers.Add(new Register(RegisterSize.SixteenBit, RegisterFlags.Load));
            registers.Add(new Register(RegisterSize.SixteenBit, RegisterFlags.All));
            registers.Add(new Register(RegisterSize.OneBit, RegisterFlags.None));

            for (var i = 0; i < MEM_SIZE; i++)
            {
                memory.Add(new MemoryBlock());
            }
        }

        public void Run()
        {
            RandomizeMemory();
        }

        private void RandomizeMemory()
        {
            var rand = new Random();

            foreach (var block in memory)
            {
                var hexCode = rand.Next(0xF);
                block.SetBits(hexCode, 0xC, 0xF);

                var nextBits = 0;
                if (hexCode == 0x7)
                {
                    nextBits = REG_REF_INSTR_NUMS[rand.Next(REG_REF_INSTR_NUMS.Length)];
                }
                else
                {
                    nextBits = rand.Next(0x1000);
                }

                block.SetBits(nextBits, 0x0, 0xB);
            }

            foreach (var block in memory)
            {
                Console.WriteLine(block.HexValue);
            }
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
            Console.ReadLine();
        }
    }
}
