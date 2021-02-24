using Classic.Cryptography;
using Microsoft.Extensions.DependencyInjection;

namespace Classic.World.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWorldServer(this IServiceCollection services)
        {
            return services
                .AddTransient<AuthCrypt>()
                .AddTransient<WorldClient>()
                .AddSingleton<WorldPacketHandler>()
                .AddSingleton<WorldServer>()
                .AddHostedService<WorldServer>();
        }
    }
}
