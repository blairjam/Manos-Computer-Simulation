use registers;

pub struct Output {
    data: [i8; 8]
}

impl Output {
    pub fn new() -> Output {
        Output { data: [0, 0, 0, 0,
                        0, 0, 0, 0] }
    }

    pub fn get_data(&self) -> [i8; 8] {
        self.data
    }
}

impl registers::DoesTick for Output {
    fn tick(&self) {
        println!("Output tick.");
    }
}

impl registers::CanLoad for Output {
    fn load(&self) {

    }
}
