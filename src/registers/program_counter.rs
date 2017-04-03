use registers::*;

pub struct ProgramCounter {
    data: [i8; THR_QTR_WORD]
}

impl ProgramCounter {
    pub fn new() -> ProgramCounter {
        ProgramCounter { data: [0; THR_QTR_WORD] }
    }

    pub fn get_data(&self) -> [i8; THR_QTR_WORD] {
        self.data
    }
}

impl DoesTick for ProgramCounter {
    fn tick(&self) {
        println!("Program Counter tick.");
    }
}

impl CanLoad for ProgramCounter {
    fn load(&self) {

    }
}

impl CanIncrement for ProgramCounter {
    fn increment(&self) {

    }
}

impl CanClear for ProgramCounter {
    fn clear(&self) {

    }
}
