pub trait CanWrite {
    fn write(&self);
}

pub trait CanRead {
    fn read(&self);
}

pub struct Memory {
    data: [i8; 4096]
}

impl CanWrite for Memory {
    fn write(&self) {

    }
}

impl CanRead for Memory {
    fn read(&self) {

    }
}
