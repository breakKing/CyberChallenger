using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Shared.Grpc.Interfaces;
using Shared.Grpc.Services;

namespace Shared.Grpc;

public static class DependencyInjection
{
    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddGrpc();

        services.AddMediator(opt => opt.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddMapping(Assembly.GetEntryAssembly()!);
        services.AddCurrentUserTracker();

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

    private static IServiceCollection AddCurrentUserTracker(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}