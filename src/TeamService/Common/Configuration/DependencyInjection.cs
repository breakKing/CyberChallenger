using System.Reflection;
using Mapster;
using MapsterMapper;
using TeamService.Features.Crud;
using TeamService.Persistence;

namespace TeamService.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddMediator(opt => opt.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddMapping();

        return services;
    }

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

    private static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        
        var mapper= new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapper);
        
        return services;
    }
}