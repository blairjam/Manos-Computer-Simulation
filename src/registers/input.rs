use registers;

pub struct Input {
    data: [i8; 8]
}

impl Input {
    pub fn new() -> Input {
        Input { data: [0, 0, 0, 0,
                       0, 0, 0, 0] }
    }

    pub fn get_data(&self) -> [i8; 8] {
        self.data
    }
}

impl registers::DoesTick for Input {
    fn tick(&self) {
        println!("Input tick.");
    }
}
