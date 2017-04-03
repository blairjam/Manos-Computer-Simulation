use registers::*;

pub struct InstructionRegister {
    data: [i8; WORD]
}

impl InstructionRegister {
    pub fn new() -> InstructionRegister {
        InstructionRegister { data: [0; WORD] }
    }

    pub fn get_data(&self) -> [i8; WORD] {
        self.data
    }
}

impl DoesTick for InstructionRegister {
    fn tick(&self) {
        println!("Instruction Register tick.");
    }
}

impl CanLoad for InstructionRegister {
    fn load(&self) {

    }
}
