extern crate manos_computer_simulation;
use manos_computer_simulation::registers::*;

fn main() {
    let mut registers: Vec<Box<DoesTick>> = Vec::new();
    registers.push(Box::new(address_register::AddressRegister::new()));
    registers.push(Box::new(program_counter::ProgramCounter::new()));
    registers.push(Box::new(data_register::DataRegister::new()));
    registers.push(Box::new(accumulator::Accumulator::new()));
    registers.push(Box::new(input::Input::new()));
    registers.push(Box::new(instruction_register::InstructionRegister::new()));
    registers.push(Box::new(temporary_register::TemporaryRegister::new()));
    registers.push(Box::new(output::Output::new()));

    for i in &registers {
        i.tick();
    }

    /*let a = 3;
    let b = 2;
    let c = mcs::add(a, b);

    println!("C={}", c);*/
}
