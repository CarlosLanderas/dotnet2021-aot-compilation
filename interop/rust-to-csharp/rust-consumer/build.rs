
const COMPILER_LIBRARIES_LOCATION : &'static str = 
"C:/Users/carlo/.nuget/packages/runtime.win-x64.microsoft.dotnet.ilcompiler/6.0.0-preview.6.21278.2/sdk";

fn main() {        

    //Link compiler libraries   
    println!("cargo:rustc-link-search=native={}", COMPILER_LIBRARIES_LOCATION);
    println!("cargo:rustc-link-search=native={}", "./lib");

    println!("cargo:rustc-link-lib=static=bootstrapperdll");    
    println!("cargo:rustc-link-lib=static=Runtime");
    println!("cargo:rustc-link-lib=static=dotnet-lib");
}

