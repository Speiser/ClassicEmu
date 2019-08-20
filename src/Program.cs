using Classic.Auth;
using Classic.World;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Classic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) => Console.WriteLine($"UnhandledException: {JsonConvert.SerializeObject(e)}");
            TaskScheduler.UnobservedTaskException += (s, e) => Console.WriteLine($"UnobservedTaskException: {JsonConvert.SerializeObject(e)}");

            _ = new AuthenticationServer().Start();
            await new WorldServer().Start();
        }
    }
}