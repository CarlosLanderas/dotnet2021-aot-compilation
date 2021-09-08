package main

/*
#include <stdint.h>
struct Comment {
    uint32_t postId;
    uint32_t id;
	char *name;
	char *body;
};
*/
import "C"
import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
)

func main() {}

type Comment struct {
	PostId uint
	Id     uint
	Name   string
	Body   string
}

//export getComment
func getComment(id int) C.struct_Comment {

	url := fmt.Sprintf("https://jsonplaceholder.typicode.com/comments/%v", id)
	fmt.Printf("Calling url: %s\n", url)
	res, err := http.Get(url)
	if err != nil {
		panic(err)
	}
	defer res.Body.Close()
	b, err := ioutil.ReadAll(res.Body)
	if err != nil {
		panic(err)
	}

	comment := &Comment{}
	err = json.Unmarshal(b, comment)
	if err != nil {
		panic(err)
	}

	return C.struct_Comment{
		postId: C.uint(comment.PostId),
		id:     C.uint(comment.Id),
		name:   C.CString(comment.Name),
		body:   C.CString(comment.Body),
	}
}

//export multiply
func multiply(num1 int, num2 int) int {
	return num1 * num2
}
