use std::{ffi::{CStr, CString}, os::raw::c_char};
#[cfg(windows)]
extern crate winapi;

#[link(name = "bootstrapperdll", kind = "static")]
#[link(name = "Runtime", kind = "static")]
#[link(name = "dotnet-lib", kind = "static")]
extern "C" {
    pub fn fibonnaci(n: i32) -> i32;
    pub fn famousphrase(name: *const c_char) -> *const c_char;
}

fn main() {
    unsafe {
        let fib1 = fibonnaci(5);
        println!("Fib1 is {}", fib1);
        let fib2 = fibonnaci(34);
        println!("Fib2 is {}", fib2);

        let name = CString::new("Carlos").unwrap();
        let name_ptr = name.as_ptr();

        let phrase1 = famousphrase(name_ptr);
        let phrase2 = famousphrase(name_ptr);

        println!("{:?}", CStr::from_ptr(phrase1));
        println!("{:?}", CStr::from_ptr(phrase2));
    }
}
