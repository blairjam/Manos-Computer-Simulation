use registers::*;

pub struct Accumulator {
    data: [i8; WORD]
}

impl Accumulator {
    pub fn new() -> Accumulator {
        Accumulator { data: [0; WORD] }
    }

    pub fn get_data(&self) -> [i8; WORD] {
        self.data
    }
}

impl DoesTick for Accumulator {
    fn tick(&self) {
        println!("Accumulator tick.");
    }
}

impl CanLoad for Accumulator {
    fn load(&self) {

    }
}

impl CanIncrement for Accumulator {
    fn increment(&self) {

    }
}

impl CanClear for Accumulator {
    fn clear(&self) {

    }
}
