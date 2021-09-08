using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace dotnet_lib
{
    public class Library
    {
        [UnmanagedCallersOnly(EntryPoint = "fibonnaci")]
        public static int fibonnaci(int num) => fib_int(num);

        static int fib_int(int n)
        {
            if (n == 0 || n == 1) return n;
            return fib_int(n - 1) + fib_int(n - 2);
        }

        [UnmanagedCallersOnly(EntryPoint = "famousphrase")]
        public static IntPtr famousphrase(IntPtr name)
        {
            var who = Marshal.PtrToStringAnsi(name);
            var phrase = FamousPhrases[rnd.Next(0, FamousPhrases.Count - 1)];
            var msg = $"Hello {who}, {phrase}";

            return Marshal.StringToHGlobalAnsi(msg);
        }
       
        static Random rnd = new Random(DateTime.UtcNow.Second);

        static List<string> FamousPhrases = new List<string> {
            "Where there is love there is life",
            "Once you choose hope, anythings possible",
            "Try to be a rainbow in someones cloud",
            "Happiness is like a kiss. You must share it to enjoy it.",
            "Love is life. And if you miss love, you miss life"
        };
    }
}
