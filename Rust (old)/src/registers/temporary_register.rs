use registers::*;

pub struct TemporaryRegister {
    data: [i8; WORD]
}

impl TemporaryRegister {
    pub fn new() -> TemporaryRegister {
        TemporaryRegister { data: [0; WORD] }
    }

    pub fn get_data(&self) -> [i8; WORD] {
        self.data
    }
}

impl DoesTick for TemporaryRegister {
    fn tick(&self) {
        println!("Temporary Register tick.");
    }
}

impl CanLoad for TemporaryRegister {
    fn load(&self) {

    }
}

impl CanIncrement for TemporaryRegister {
    fn increment(&self) {

    }
}

impl CanClear for TemporaryRegister {
    fn clear(&self) {

    }
}
