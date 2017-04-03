use registers;

pub struct Accumulator {
    data: [i8; 16]
}

impl Accumulator {
    pub fn new() -> Accumulator {
        Accumulator { data: [0, 0, 0, 0,
                             0, 0, 0, 0,
                             0, 0, 0, 0,
                             0, 0, 0, 0] }
    }

    pub fn get_data(&self) -> [i8; 16] {
        self.data
    }
}

impl registers::DoesTick for Accumulator {
    fn tick(&self) {
        println!("Accumulator tick.");
    }
}

impl registers::CanLoad for Accumulator {
    fn load(&self) {

    }
}

impl registers::CanIncrement for Accumulator {
    fn increment(&self) {

    }
}

impl registers::CanClear for Accumulator {
    fn clear(&self) {

    }
}
