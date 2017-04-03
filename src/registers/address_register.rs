use registers;

pub struct AddressRegister {
    data: [i8; 12]
}

impl AddressRegister {
    pub fn new() -> AddressRegister {
        AddressRegister { data: [0, 0, 0, 0,
                                 0, 0, 0, 0,
                                 0, 0, 0, 0] }
    }

    pub fn get_data(&self) -> [i8; 12] {
        self.data
    }
}

impl registers::DoesTick for AddressRegister {
    fn tick(&self) {
        println!("Address Register tick.");
    }
}

impl registers::CanLoad for AddressRegister {
    fn load(&self) {

    }
}

impl registers::CanIncrement for AddressRegister {
    fn increment(&self) {

    }
}

impl registers::CanClear for AddressRegister {
    fn clear(&self) {

    }
}
