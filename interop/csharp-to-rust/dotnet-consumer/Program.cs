using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dotnet_consumer
{
    class Program
    {
#if WINDOWS
    const string NativeLib = "rust_lib.dll";
#else
     const string NativeLib = "librust_lib.so";
#endif

        [DllImport(NativeLib)]
        public static extern User get_request(Int32 userId);
        [DllImport(NativeLib)]
        public static extern Int32 divide(Int32 num1, Int32 num2);

        [DllImport(NativeLib)]
        public static extern Int32 fib(Int32 num1);
        [DllImport(NativeLib)]
        public static extern void free_alloc();

        static void Main(string[] args)
        {
            var st = Stopwatch.StartNew();

            for (int i = 1; i < 10; i++)
            {
                var user = get_request(i);
                var name = Marshal.PtrToStringAnsi(user.name);
                var email = Marshal.PtrToStringAnsi(user.email);
                free_alloc();
                Console.WriteLine($"Retrieved user with id {user.id}, name: {name} and email: {email}");
            }

            for (int i = 1; i < 10000; i++)
            {
                divide(i, 5);              
            }


            for (int i = 1; i < 20; i++)
            {              
                fib(i);
            }

            var time = Process.GetCurrentProcess().StartTime;
            Console.WriteLine($"Elapsed: {(DateTime.Now - time).TotalMilliseconds}");

        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct User
    {
        public Int16 id;
        public IntPtr name;
        public IntPtr email;
    }
}
