namespace IdentityProvider.Features.Connect;

public static class DependencyInjection
{
    public static IServiceCollection AddOpenIdConnectFeatures(this IServiceCollection services)
    {
        services.AddScoped<IClaimsPrincipalService, ClaimsPrincipalService>();
        
        return services;
    }
}