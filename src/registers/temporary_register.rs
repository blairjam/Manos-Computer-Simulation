use registers;

pub struct TemporaryRegister {
    data: [i8; 16]
}

impl TemporaryRegister {
    pub fn new() -> TemporaryRegister {
        TemporaryRegister { data: [0, 0, 0, 0,
                                   0, 0, 0, 0,
                                   0, 0, 0, 0,
                                   0, 0, 0, 0] }
    }

    pub fn get_data(&self) -> [i8; 16] {
        self.data
    }
}

impl registers::DoesTick for TemporaryRegister {
    fn tick(&self) {
        println!("Temporary Register tick.");
    }
}

impl registers::CanLoad for TemporaryRegister {
    fn load(&self) {

    }
}

impl registers::CanIncrement for TemporaryRegister {
    fn increment(&self) {

    }
}

impl registers::CanClear for TemporaryRegister {
    fn clear(&self) {

    }
}
