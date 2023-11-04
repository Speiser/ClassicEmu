using Classic.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Classic.Auth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationServer(this IServiceCollection services)
    {
        return services
            .AddTransient<LoginClient>()
            .AddSingleton(s => new AuthDatabase(s.GetService<IConfiguration>().GetConnectionString("auth-db")))
            .AddHostedService<AuthenticationServer>();
    }
}
