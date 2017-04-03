pub mod address_register;
pub mod program_counter;
pub mod data_register;
pub mod accumulator;
pub mod instruction_register;
pub mod temporary_register;
pub mod input;
pub mod output;

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
