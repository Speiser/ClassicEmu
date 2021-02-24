using System.Threading.Tasks;
using Classic.Auth.Extensions;
using Classic.Common;
using Classic.World.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Classic
{
    class Program
    {
        static async Task Main()
        {
            await DataStore.Init();

            await Host.CreateDefaultBuilder()
                .ConfigureLogging(logging => logging
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Trace))
                .ConfigureServices(services => services
                    .AddSingleton<ErrorHandler>()
                    .AddAuthenticationServer()
                    .AddWorldServer())
                .RunConsoleAsync();
        }
    }
}