use registers;

pub struct ProgramCounter {
    data: [i8; 12]
}

impl ProgramCounter {
    pub fn new() -> ProgramCounter {
        ProgramCounter { data: [0, 0, 0, 0,
                                0, 0, 0, 0,
                                0, 0, 0, 0] }
    }

    pub fn get_data(&self) -> [i8; 12] {
        self.data
    }
}

impl registers::DoesTick for ProgramCounter {
    fn tick(&self) {
        println!("Program Counter tick.");
    }
}

impl registers::CanLoad for ProgramCounter {
    fn load(&self) {

    }
}

impl registers::CanIncrement for ProgramCounter {
    fn increment(&self) {

    }
}

impl registers::CanClear for ProgramCounter {
    fn clear(&self) {

    }
}
