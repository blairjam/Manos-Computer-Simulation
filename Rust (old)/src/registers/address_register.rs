use registers::*;

pub struct AddressRegister {
    data: [i8; THR_QTR_WORD]
}

impl AddressRegister {
    pub fn new() -> AddressRegister {
        AddressRegister { data: [0; THR_QTR_WORD] }
    }

    pub fn get_data(&self) -> [i8; THR_QTR_WORD] {
        self.data
    }
}

impl DoesTick for AddressRegister {
    fn tick(&self) {
        println!("Address Register tick.");
    }
}

impl CanLoad for AddressRegister {
    fn load(&self) {

    }
}

impl CanIncrement for AddressRegister {
    fn increment(&self) {

    }
}

impl CanClear for AddressRegister {
    fn clear(&self) {

    }
}
