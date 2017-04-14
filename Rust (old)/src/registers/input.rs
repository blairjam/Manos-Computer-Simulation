use registers::*;

pub struct Input {
    data: [i8; HALF_WORD]
}

impl Input {
    pub fn new() -> Input {
        Input { data: [0; HALF_WORD] }
    }

    pub fn get_data(&self) -> [i8; HALF_WORD] {
        self.data
    }
}

impl DoesTick for Input {
    fn tick(&self) {
        println!("Input tick.");
    }
}
