use registers;

pub struct DataRegister {
    data: [i8; 16]
}

impl DataRegister {
    pub fn new() -> DataRegister {
        DataRegister { data: [0, 0, 0, 0,
                              0, 0, 0, 0,
                              0, 0, 0, 0,
                              0, 0, 0, 0] }
    }

    pub fn get_data(&self) -> [i8; 16] {
        self.data
    }
}

impl registers::DoesTick for DataRegister {
    fn tick(&self) {
        println!("Data Register tick.");
    }
}

impl registers::CanLoad for DataRegister {
    fn load(&self) {

    }
}

impl registers::CanIncrement for DataRegister {
    fn increment(&self) {

    }
}

impl registers::CanClear for DataRegister {
    fn clear(&self) {

    }
}
