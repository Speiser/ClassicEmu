using Classic.Shared.Services;
using Classic.World.Packets;
using Classic.World.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Classic.World.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorldServer(this IServiceCollection services)
    {
        return services
            .AddSingleton(s => new AuthDatabase(s.GetService<IConfiguration>().GetConnectionString("auth-db")))
            .AddSingleton(s => new WorldDatabase(s.GetService<IConfiguration>().GetConnectionString("world-db")))
            .AddSingleton<CharacterService>()
            .AddTransient<WorldClient>()
            .AddSingleton<IWorldManager, WorldManager>()
            .AddSingleton<WorldPacketHandler>()
            .AddSingleton<WorldServer>()
            .AddHostedService(s => s.GetService<WorldServer>());
    }
}
