using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManosComputer
{
    public class Program
    {
        public const int WORD_SIZE = 0x10;

        private const int MEM_SIZE = 0x1000;

        private const int ADDRESS_REGISTER = 0;
        private const int PROGRAM_CONTROL = 1;
        private const int DATA_REGISTER = 2;
        private const int ACCUMULATOR = 3;
        private const int INSTRUCTION_REGISTER = 4;
        private const int TEMPORARY_REGISTER = 5;
        private const int E_REGISTER = 6;

        private static readonly int[] REG_REF_INSTR_NUMS = { 0x800, 0x400, 0x200, 0x100,
                                                             0x080, 0x040, 0x020, 0x010,
                                                             0x008, 0x004, 0x002, 0x001 };

        private const int HEADER_PADDING = 50;

        private List<Register> registers = new List<Register>();
        private List<MemoryBlock> memory = new List<MemoryBlock>();

        public Program()
        {
            // Set up all the registers.
            registers.Add(new Register(RegisterSize.TwelveBit, "AR"));
            registers.Add(new Register(RegisterSize.TwelveBit, "PC"));
            registers.Add(new Register(RegisterSize.SixteenBit, "DR"));
            registers.Add(new Register(RegisterSize.SixteenBit, "AC"));
            registers.Add(new Register(RegisterSize.SixteenBit, "IR"));
            registers.Add(new Register(RegisterSize.SixteenBit, "TR"));
            registers.Add(new Register(RegisterSize.OneBit, "E"));

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
                PrintRegisterTableHeader();
                PrintRegisterTableValues("Initial Values");

                // T0: PC -> AR
                registers[ADDRESS_REGISTER].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue);
                PrintRegisterTableValues("T0: AR <- PC");

                // T1: M[AR] -> IR, PC -> PC + 1
                registers[INSTRUCTION_REGISTER].SetAllBitsFromValue(memory[registers[ADDRESS_REGISTER].DecValue].DecValue);
                registers[PROGRAM_CONTROL].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue + 1);
                PrintRegisterTableValues("T1: IR <- M[AR], PC <- PC + 1");

                // T2: IR(0-11) -> AR, decode IR(12-15)
                registers[ADDRESS_REGISTER].SetAllBitsFromValue(registers[INSTRUCTION_REGISTER].DecValue);
                PrintRegisterTableValues("T2: AR <- IR(0-11)");

                if (registers[INSTRUCTION_REGISTER].DecValue >= 0x7000 && registers[INSTRUCTION_REGISTER].DecValue <= 0x7FFF)
                {
                    RunRegisterInstruction();
                }
                else
                {
                    RunMemoryInstruction();
                }

                Console.WriteLine();
            }
        }

        private void RunRegisterInstruction()
        {
            // T3 Execute Instruction
            var instruction = registers[INSTRUCTION_REGISTER].DecValue - 0x7000;
            var action = "T3: ";

            // 0x800
            if (instruction == REG_REF_INSTR_NUMS[0])
            {
                registers[ACCUMULATOR].SetAllBitsFromValue(0);
                action += "AC <- 0";
            }
            // 0x400
            else if (instruction == REG_REF_INSTR_NUMS[1])
            {
                registers[E_REGISTER].SetAllBitsFromValue(0);
                action += "E <- 0";
            }
            // 0x200
            else if (instruction == REG_REF_INSTR_NUMS[2])
            {
                for (var i = 0; i < registers[ACCUMULATOR].Data.Length; i++)
                {
                    registers[ACCUMULATOR].Data[i] = (byte)~registers[ACCUMULATOR].Data[i];
                }

                action += "AC <- !AC";
            }
            // 0x100
            else if (instruction == REG_REF_INSTR_NUMS[3])
            {
                for (var i = 0; i < registers[E_REGISTER].Data.Length; i++)
                {
                    registers[E_REGISTER].Data[i] = (byte)~registers[E_REGISTER].Data[i];
                }

                action += "E <- !E";
            }
            // 0x080
            else if (instruction == REG_REF_INSTR_NUMS[4])
            {
                registers[E_REGISTER].SetAllBitsFromValue(registers[ACCUMULATOR].Data[0]);
                registers[ACCUMULATOR].SetAllBitsFromValue(registers[ACCUMULATOR].DecValue >> 1);
                registers[ACCUMULATOR].Data[0xF] = registers[E_REGISTER].Data[0];

                action += "E <- AC(0), AC <- shr AC, AC(15) <- E";
            }
            // 0x040
            else if (instruction == REG_REF_INSTR_NUMS[5])
            {
                registers[E_REGISTER].SetAllBitsFromValue(registers[ACCUMULATOR].Data[0xF]);
                registers[ACCUMULATOR].SetAllBitsFromValue(registers[ACCUMULATOR].DecValue << 1);
                registers[ACCUMULATOR].Data[0] = registers[E_REGISTER].Data[0];

                action += "E <- AC(15), AC <- shl AC, AC(0) <- E";
            }
            // 0x020
            else if (instruction == REG_REF_INSTR_NUMS[6])
            {
                registers[ACCUMULATOR].SetAllBitsFromValue(registers[ACCUMULATOR].DecValue + 1);
                action += "AC <- AC + 1";
            }
            // 0x010
            else if (instruction == REG_REF_INSTR_NUMS[7])
            {
                if (registers[ACCUMULATOR].Data[0xF] == 0)
                {
                    registers[PROGRAM_CONTROL].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue + 1);
                }

                action += "if AC(15) = 0 then PC <- PC + 1";
            }
            // 0x008
            else if (instruction == REG_REF_INSTR_NUMS[8])
            {
                if (registers[ACCUMULATOR].Data[0xF] == 1)
                {
                    registers[PROGRAM_CONTROL].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue + 1);
                }

                action += "if AC(15) = 1 then PC <- PC + 1";
            }
            // 0x004
            else if (instruction == REG_REF_INSTR_NUMS[9])
            {
                if (registers[ACCUMULATOR].DecValue == 0)
                {
                    registers[PROGRAM_CONTROL].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue + 1);
                }

                action += "if AC = 0 then PC <- PC + 1";
            }
            // 0x002
            else if (instruction == REG_REF_INSTR_NUMS[10])
            {
                if (registers[E_REGISTER].DecValue == 0)
                {
                    registers[PROGRAM_CONTROL].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue + 1);
                }

                action += "if E = 0 then PC <- PC + 1";
            }
            // 0x001
            else if (instruction == REG_REF_INSTR_NUMS[11])
            {
                // S goes to 0.
                action += "S <- 0";
            }

            PrintRegisterTableValues(action);
        }

        private void RunMemoryInstruction()
        {
            var irValue = registers[INSTRUCTION_REGISTER].DecValue;

            // T3 M[AR] -> AR
            if (irValue >= 0x8000 && irValue <= 0xEFFF)
            {
                registers[ADDRESS_REGISTER].SetAllBitsFromValue(memory[registers[ADDRESS_REGISTER].DecValue % MEM_SIZE].DecValue);
                irValue -= 0x8000;

                PrintRegisterTableValues("T3: AR <- M[AR]");
            }
            else
            {
                PrintRegisterTableValues("T3: NA");
            }

            // AND
            if (irValue <= 0x0FFF)
            {
                // T4 M[AR] -> DR
                registers[DATA_REGISTER].SetAllBitsFromValue(memory[registers[ADDRESS_REGISTER].DecValue % MEM_SIZE].DecValue);
                PrintRegisterTableValues("T4: DR <- M[AR]");

                // T5 AC & DR -> AC
                registers[ACCUMULATOR].SetAllBitsFromValue(registers[ACCUMULATOR].DecValue & registers[DATA_REGISTER].DecValue);
                PrintRegisterTableValues("T5: AC <- AC ^ DR");
            }
            // ADD
            else if(irValue >= 0x1000 && irValue <= 0x1FFF)
            {
                // T4 M[AR] -> DR
                registers[DATA_REGISTER].SetAllBitsFromValue(memory[registers[ADDRESS_REGISTER].DecValue % MEM_SIZE].DecValue);
                PrintRegisterTableValues("T4: DR <- M[AR]");

                var sum = registers[ACCUMULATOR].DecValue + registers[DATA_REGISTER].DecValue;

                // T5 AC + AR -> AC, Carry bit -> E
                registers[ACCUMULATOR].SetAllBitsFromValue(sum);
                registers[E_REGISTER].SetAllBitsFromValue(sum - 0xFFFF > 0 ? 1 : 0);
                PrintRegisterTableValues("T5: AC <- AC + AR, E <- Carry Out");
            }
            // LDA
            else if (irValue >= 0x2000 && irValue <= 0x2FFF)
            {
                // T4 M[AR] -> DR
                registers[DATA_REGISTER].SetAllBitsFromValue(memory[registers[ADDRESS_REGISTER].DecValue % MEM_SIZE].DecValue);
                PrintRegisterTableValues("T4: DR <- M[AR]");

                // T5 DR -> AC
                registers[ACCUMULATOR].SetAllBitsFromValue(registers[DATA_REGISTER].DecValue);
                PrintRegisterTableValues("T5: AC <- DR");
            }
            // STA
            else if (irValue >= 0x3000 && irValue <= 0x3FFF)
            {
                // T4 AC -> M[AR]
                memory[registers[ADDRESS_REGISTER].DecValue % MEM_SIZE].SetAllBitsFromValue(registers[ACCUMULATOR].DecValue);
                PrintRegisterTableValues("T4: M[AR] <- AC");
            }
            // BUN
            else if (irValue >= 0x4000 && irValue <= 0x4FFF)
            {
                // T4 AR -> PC
                registers[PROGRAM_CONTROL].SetAllBitsFromValue(registers[ADDRESS_REGISTER].DecValue);
                PrintRegisterTableValues("T4: PC <- AR");
            }
            // BSA 
            else if (irValue >= 0x5000 && irValue <= 0x5FFF)
            {
                // T4 PC -> M[AR], AR + 1 -> AR
                memory[registers[ADDRESS_REGISTER].DecValue % MEM_SIZE].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue);
                registers[ADDRESS_REGISTER].SetAllBitsFromValue(registers[ADDRESS_REGISTER].DecValue + 1);
                PrintRegisterTableValues("T4: M[AR] <- PC, AR <- AR + 1");

                // T5 AR -> PC
                registers[PROGRAM_CONTROL].SetAllBitsFromValue(registers[ADDRESS_REGISTER].DecValue);
                PrintRegisterTableValues("T5: PC <- AR");
            }
            // ISZ
            else if (irValue >= 0x6000 && irValue <= 0x6FFF)
            {
                // T4 M[AR] -> DR
                registers[DATA_REGISTER].SetAllBitsFromValue(memory[registers[ADDRESS_REGISTER].DecValue % MEM_SIZE].DecValue);
                PrintRegisterTableValues("T4: DR <- M[AR]");

                // T5 DR + 1 -> DR
                registers[DATA_REGISTER].SetAllBitsFromValue(registers[DATA_REGISTER].DecValue + 1);
                PrintRegisterTableValues("T5: DR <- DR + 1");

                // T6: DR -> M[AR], if DR=0 then PC + 1 -> PC
                memory[registers[ADDRESS_REGISTER].DecValue % MEM_SIZE].SetAllBitsFromValue(registers[DATA_REGISTER].DecValue);
                if (registers[DATA_REGISTER].DecValue == 0)
                {
                    registers[PROGRAM_CONTROL].SetAllBitsFromValue(registers[PROGRAM_CONTROL].DecValue + 1);
                }
                PrintRegisterTableValues("T6: M[AR] <- DR, if DR = 0 then PC <- PC + 1");
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

        private void PrintRegisterTableHeader()
        {
            var head = "Register Transfer Statement";
            Console.Write("| " + head.PadRight(HEADER_PADDING) + " | ");

            foreach (var register in registers)
            {
                Console.Write(register.Abbr.PadRight(4) + " | ");
            }

            Console.WriteLine("M[AR]|");
        }

        private void PrintRegisterTableValues(string statement)
        {
            Console.Write("| " + statement.PadRight(HEADER_PADDING) + " | ");

            foreach (var register in registers)
            {
                Console.Write(register.HexValue + " | ");
            }

            var memVal = memory[registers[ADDRESS_REGISTER].DecValue % MEM_SIZE].HexValue;

            Console.WriteLine(memVal + " |");
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
            Console.ReadLine();
        }
    }
}
