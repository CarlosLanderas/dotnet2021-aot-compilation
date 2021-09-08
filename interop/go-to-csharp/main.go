package main

// #cgo CFLAGS: -I${SRCDIR}/lib
// #cgo LDFLAGS: -L${SRCDIR}/lib -Wl,-rpath=\$ORIGIN/lib  -lbootstrapperdll -lRuntime -ldotnet-lib
//extern int fibonnaci(int n);
//extern char * famousphrase(char *name);
import "C"
import "fmt"

func main() {
	res := C.fibonnaci(10)
	fmt.Printf("Fib result is: %v\n", res)
	res2 := C.famousphrase(C.CString("Lander!"))
	fmt.Println(C.GoString(res2))
}
