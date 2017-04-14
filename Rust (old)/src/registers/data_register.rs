use registers::*;

pub struct DataRegister {
    data: [i8; WORD]
}

impl DataRegister {
    pub fn new() -> DataRegister {
        DataRegister { data: [0; WORD] }
    }

    pub fn get_data(&self) -> [i8; WORD] {
        self.data
    }
}

impl DoesTick for DataRegister {
    fn tick(&self) {
        println!("Data Register tick.");
    }
}

impl CanLoad for DataRegister {
    fn load(&self) {

    }
}

impl CanIncrement for DataRegister {
    fn increment(&self) {

    }
}

impl CanClear for DataRegister {
    fn clear(&self) {

    }
}
