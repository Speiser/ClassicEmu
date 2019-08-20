using Microsoft.Extensions.DependencyInjection;

namespace Classic.World.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWorldServer(this IServiceCollection services)
        {
            return services
                .AddSingleton<WorldServer>()
                .AddTransient<WorldClient>()
                .AddSingleton<WorldPacketHandler>();
        }
    }
}
