using TeamService.Features.Crud;
using TeamService.Persistence;

namespace TeamService.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        
        return services;
    }

    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddCrudFeatures();
        
        return services;
    }
}