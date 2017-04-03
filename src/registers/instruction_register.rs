use registers;

pub struct InstructionRegister {
    data: [i8; 16]
}

impl InstructionRegister {
    pub fn new() -> InstructionRegister {
        InstructionRegister { data: [0, 0, 0, 0,
                                     0, 0, 0, 0,
                                     0, 0, 0, 0,
                                     0, 0, 0, 0] }
    }

    pub fn get_data(&self) -> [i8; 16] {
        self.data
    }
}

impl registers::DoesTick for InstructionRegister {
    fn tick(&self) {
        println!("Instruction Register tick.");
    }
}

impl registers::CanLoad for InstructionRegister {
    fn load(&self) {

    }
}
