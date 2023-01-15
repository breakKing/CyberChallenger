using IdentityProviderService.Persistence;

namespace IdentityProviderService.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        
        return services;
    }

    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        
        
        return services;
    }
}