import ctypes

class Comment(ctypes.Structure):
    _fields_ = [('postId', ctypes.c_uint32),
                ('id', ctypes.c_uint32),
                ('name', ctypes.POINTER(ctypes.c_char)),
                ('body', ctypes.POINTER(ctypes.c_char))]

so = ctypes.cdll.LoadLibrary('libgolib.so')
multiply = so.multiply
multiply.argtypes = [ctypes.c_int, ctypes.c_int]
multiply.restype = ctypes.c_int

get_comment = so.getComment
get_comment.argtypes = [ctypes.c_int]
get_comment.restype = Comment

res = multiply(25, 3)
print(res)

comment = get_comment(4)

id = ctypes.cast(comment.id, ctypes.c_void_p).value
postId = ctypes.cast(comment.postId, ctypes.c_void_p).value
name = ctypes.string_at(comment.name)
body = ctypes.string_at(comment.body)
print('postId: %s\nid:%s\nname:%s\nbody %s' % (postId, id, name, body))