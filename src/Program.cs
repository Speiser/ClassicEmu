using Classic.Auth;
using Classic.World;
using System.Threading;

namespace Classic
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(() => new AuthenticationServer().Start()).Start();
            new WorldServer().Start();
            // while (true) Thread.Sleep(100);
        }
    }
}