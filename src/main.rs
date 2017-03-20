extern crate manos_computer_simulation;
use manos_computer_simulation::mcs;

fn main() {
    let a = 3;
    let b = 2;
    let c = mcs::add(a, b);

    println!("C={}", c);
}
