use serde_derive::Deserialize;
use std::{ffi::CString, os::raw::c_char};

static mut NAME_POINTER: *mut c_char = 0 as *mut c_char;
static mut EMAIL_POINTER: *mut c_char = 0 as *mut c_char;

#[no_mangle]
pub unsafe extern "C" fn get_request(user_id: i32) -> UserStruct {
    let url = format!("https://jsonplaceholder.typicode.com/users/{}", user_id);
    let user_result: UserResponse = reqwest::blocking::get(url)
        .unwrap()
        .json::<UserResponse>()
        .unwrap();

    let name = CString::new(user_result.name.as_str()).unwrap().into_raw();
    let email = CString::new(user_result.email.as_str()).unwrap().into_raw();

    NAME_POINTER = name as *mut c_char;
    EMAIL_POINTER = email as *mut c_char;

    UserStruct {
        id: user_result.id,
        name,
        email,
    }
}

#[no_mangle]
pub unsafe extern "C" fn free_alloc() {
    let _ = CString::from_raw(NAME_POINTER);
    let _ = CString::from_raw(EMAIL_POINTER);
    EMAIL_POINTER = 0 as *mut c_char;
    NAME_POINTER = 0 as *mut c_char;
}

#[no_mangle]
pub extern "C" fn divide(num1: i32, num2: i32) -> i32 {
    num1 / num2
}

#[no_mangle]
pub extern "C" fn fib(num1: i32) -> i32 {
    return fib_int(num1);
}

fn fib_int(n: i32) -> i32 {
    if n == 0 || n == 1 {
        return n;
    }

    fib_int(n - 1) + fib(n - 2)
}


#[derive(Deserialize, Debug)]
pub struct UserResponse {
    pub id: i16,
    pub name: String,
    pub email: String,
}

#[repr(C)]
pub struct UserStruct {
    pub id: i16,
    pub name: *mut c_char,
    pub email: *mut c_char,
}
