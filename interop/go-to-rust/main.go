package main

// #cgo CFLAGS: -I${SRCDIR}/include
// #cgo LDFLAGS: -L${SRCDIR}/lib -Wl,-rpath=\$ORIGIN/lib  -lrust_lib
// #include <librust_lib.h>
import "C"
import (
	"fmt"
	"log"
	"os"
	"strconv"
)

func main() {
	fmt.Println("Running")
	ret := C.divide(56, 2)
	fib := C.fib(5)
	fmt.Println(ret)
	fmt.Println(fib)

	request_id := os.Args[1]
	id, err := strconv.Atoi(request_id)
	if err != nil {
		log.Fatal("Invalid request id parameter, please provide a valid number")
	}

	res := C.get_request(C.int(id))

	email := C.GoString(res.email)
	name := C.GoString(res.name)
	fmt.Printf("User id: %v, name: %s, email: %s", res.id, email, name)
	C.free_alloc()

}

type UserStruct struct {
	id    int
	name  *C.char
	email *C.char
}
