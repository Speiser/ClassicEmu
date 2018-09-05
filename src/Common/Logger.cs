using System;
using System.Text;

namespace Classic.Common {
    public static class Logger {
        public static void Log(string obj) {
            Console.WriteLine($"[{DateTime.Now}] {obj}");
        }
        public static void LogPacket(byte[] packet) {
            Log($"Packet received {packet.Length} bytes");
            // Log(Encoding.ASCII.GetString(packet));
        }
    }
}