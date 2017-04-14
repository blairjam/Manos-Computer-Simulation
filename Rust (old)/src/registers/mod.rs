pub mod address_register;
pub mod program_counter;
pub mod data_register;
pub mod accumulator;
pub mod instruction_register;
pub mod temporary_register;
pub mod input;
pub mod output;

pub const WORD: usize = 16;
pub const THR_QTR_WORD: usize = 12;
pub const HALF_WORD: usize = 8;

pub trait DoesTick {
    fn tick(&self);
}

pub trait CanLoad {
    fn load(&self);
}

pub trait CanIncrement {
    fn increment(&self);
}

pub trait CanClear {
    fn clear(&self);
}
