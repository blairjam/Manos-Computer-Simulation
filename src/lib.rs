pub mod mcs {
    pub fn add(a: i32, b: i32) -> i32 {
        a + b
    }
}

#[cfg(test)]
mod test {
    use mcs::*;

    #[test]
    fn add_one_two() {
        let a = 1;
        let b = 2;
         assert_eq!(a + b, add(a, b));
    }
}
