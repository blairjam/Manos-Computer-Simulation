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
            // Set up all the registers.
            registers.Add(new Register(RegisterSize.TwelveBit, RegisterFlags.All)); // AR
            registers.Add(new Register(RegisterSize.TwelveBit, RegisterFlags.All)); // PC
            registers.Add(new Register(RegisterSize.SixteenBit, RegisterFlags.All)); // DR
            registers.Add(new Register(RegisterSize.SixteenBit, RegisterFlags.All)); // AC
            registers.Add(new Register(RegisterSize.SixteenBit, RegisterFlags.Load)); // IR
            registers.Add(new Register(RegisterSize.SixteenBit, RegisterFlags.All)); // TR
            registers.Add(new Register(RegisterSize.OneBit, RegisterFlags.None)); // E

            // Initialize the memory.
            for (var i = 0; i < MEM_SIZE; i++)
            {
                memory.Add(new MemoryBlock());
            }
        }

        public void Run()
        {
            RandomizeMemory();
            RandomizeRegisters();

            for (var i = 0; i < 5; i++)
            {
                // T0: PC -> AR
                registers[ADDRESS_REGISTER].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue);

                // T1: M[AR] -> IR, PC -> PC + 1
                registers[INSTRUCTION_REGISTER].SetAllBitsFromValue(memory[registers[ADDRESS_REGISTER].DecValue].DecValue);
                registers[PROGRAM_CONTROL].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue + 1);

                // T2: IR(0-11) -> AR, decode IR(12-15)
                registers[ADDRESS_REGISTER].SetAllBitsFromValue(registers[INSTRUCTION_REGISTER].DecValue);
            }
        }

        private void RandomizeMemory()
        {
            var rand = new Random();

            foreach (var block in memory)
            {
                // Get the initial hex code, from 0 - E.
                var hexCode = rand.Next(0xF);

                // Set the first 4 most signifigant bits.
                block.SetBitsFromValueWithBound(hexCode, 0xC, 0xF);

                var lowBits = 0;

                // Choose the next bits from the possible register reference codes if the initial hex code is 7.
                if (hexCode == 0x7)
                {
                    lowBits = REG_REF_INSTR_NUMS[rand.Next(REG_REF_INSTR_NUMS.Length)];
                }
                // Completely select a random memory location from 0 - FFF.
                else
                {
                    lowBits = rand.Next(0x1000);
                }
                
                // Set the lower 12 bits.
                block.SetBitsFromValueWithBound(lowBits, 0x0, 0xB);
            }
        }

        private void RandomizeRegisters()
        {
            var rand = new Random();

            foreach (var register in registers)
            {
                // Set the bits of the register to a random number, bounded by 2 to the power of the length of the register data array.
                // Ex. 2^16 or 2^12 or 2^1
                register.SetAllBitsFromValue(rand.Next((int)Math.Pow(2, register.Data.Length)));
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
