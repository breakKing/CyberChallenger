namespace IdentityProviderService.Features.Connect;

public static class DependencyInjection
{
    public static IServiceCollection AddOpenIdConnectFeatures(this IServiceCollection services)
    {
        services.AddSingleton<IClaimsPrincipalService, ClaimsPrincipalService>();
        
        return services;
    }
}