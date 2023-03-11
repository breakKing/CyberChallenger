using System.Reflection;
using Mapster;
using MapsterMapper;
using TeamService.EventSourcing;
using TeamService.Features.Crud;
using TeamService.Persistence;

namespace TeamService.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddEventSourcing(configuration);
        
        return services;
    }

    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddMediator(opt =>
        {
            opt.ServiceLifetime = ServiceLifetime.Scoped;
        });
        services.AddMapping(Assembly.GetExecutingAssembly());
        
        services.AddCrudFeatures();
        
        return services;
    }
    
    private static IServiceCollection AddMapping(this IServiceCollection services, params Assembly[] assemblies)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(assemblies);
        
        var mapper= new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapper);
        
        return services;
    }
}