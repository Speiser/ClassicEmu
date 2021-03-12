using System.Threading.Tasks;
using Classic.Shared;
using Classic.Shared.Extensions;
using Classic.World.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Classic.World
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                .ConfigureLogging(logging => logging
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace))
                .ConfigureServices(services => services
                    .AddSingleton<ErrorHandler>()
                    .AddSharedServices()
                    .AddWorldServer())
                .RunConsoleAsync();
        }
    }
}