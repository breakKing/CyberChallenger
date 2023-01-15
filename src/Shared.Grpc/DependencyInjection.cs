using Microsoft.Extensions.DependencyInjection;
using Shared.Grpc.Interfaces;
using Shared.Grpc.Services;

namespace Shared.Grpc;

public static class DependencyInjection
{
    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddGrpc();
        services.AddCurrentUserTracker();

        return services;
    }

    private static IServiceCollection AddCurrentUserTracker(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();

        return services;
    }
}