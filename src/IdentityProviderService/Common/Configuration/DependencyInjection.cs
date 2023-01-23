using FastEndpoints;
using IdentityProviderService.Persistence;
using IdentityProviderService.Persistence.Entities;

namespace IdentityProviderService.Common.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration.GetConnectionString("Database")!);

        return services;
    }

    public static IServiceCollection AddMainServices(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddOpenId(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<IdentityContext>()
                    .ReplaceDefaultEntities<Application, Authorization, Scope, Token, Guid>();
            });

        return services;
    }
}