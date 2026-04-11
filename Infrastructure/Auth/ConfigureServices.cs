using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Auth;

public static class ConfigureServices
{
    public static IServiceCollection AddBasicAuthentication(this IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy(BasicAuthenticationHandler.SchemeName, policy =>
            {
                policy.AddAuthenticationSchemes(BasicAuthenticationHandler.SchemeName);
                policy.RequireAuthenticatedUser();
            });
        });

        services.AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
                BasicAuthenticationHandler.SchemeName, null);

        return services;
    }
}
