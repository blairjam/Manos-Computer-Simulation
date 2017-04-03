use registers::*;

pub struct Output {
    data: [i8; HALF_WORD]
}

impl Output {
    pub fn new() -> Output {
        Output { data: [0; HALF_WORD] }
    }

    pub fn get_data(&self) -> [i8; HALF_WORD] {
        self.data
    }
}

impl DoesTick for Output {
    fn tick(&self) {
        println!("Output tick.");
    }
}

impl CanLoad for Output {
    fn load(&self) {

    }
}
