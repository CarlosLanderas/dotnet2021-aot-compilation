using System;
using System.Runtime.InteropServices;
namespace invoker
{  
    class Program
    {
        [DllImport("golib.so")]
        public static extern int multiply(int num1, int num2);
         [DllImport("golib.so")]
        public static extern User getComment(int id);

        static void Main(string[] args)
        {
            var result = multiply(5 , 16);
            Console.WriteLine($"Result is {result}");

            var c = getComment(2);
            var name = Marshal.PtrToStringAnsi(c.name);
            var body = Marshal.PtrToStringAnsi(c.body);
           
           Console.WriteLine($"id {c.id} userid: {c.postId} name: {name} body: {body}");
            
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct User
    {
        public UInt32 postId;
        public UInt32 id;        
        public IntPtr name;
        public IntPtr body;
    }
}
