using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClassicEmu.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var s = new Server(IPAddress.Loopback, 5001);
            s.Start();
        }
    }
}
