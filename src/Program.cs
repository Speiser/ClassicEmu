using System;
using System.Threading.Tasks;
using Classic.Auth;
using Classic.Auth.Extensions;
using Classic.Common;
using Classic.World;
using Classic.World.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Classic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = RegisterServices();

            _ = services.GetService<AuthenticationServer>().Start();
            await services.GetService<WorldServer>().Start();
        }

        private static IServiceProvider RegisterServices()
        {
            return new ServiceCollection()
                .AddSingleton<ErrorHandler>()
                .AddAuthenticationServer()
                .AddWorldServer()
                .AddLogging(c => c.AddConsole().SetMinimumLevel(LogLevel.Trace))
                .BuildServiceProvider();
        }
    }
}