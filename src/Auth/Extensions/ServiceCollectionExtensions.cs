using Microsoft.Extensions.DependencyInjection;

namespace Classic.Auth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationServer(this IServiceCollection services)
    {
        return services
            .AddTransient<LoginClient>()
            .AddHostedService<AuthenticationServer>();
    }
}
