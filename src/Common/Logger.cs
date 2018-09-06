using System;

namespace Classic.Common
{
    public static class Logger
    {
        public static void Log(string obj)
        {
            Console.WriteLine($"[{DateTime.Now}] {obj}");
        }
    }
}