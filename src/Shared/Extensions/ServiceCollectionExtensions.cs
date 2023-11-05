using Classic.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Classic.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<AccountService>()
            .AddSingleton<RealmlistService>();
    }
}
