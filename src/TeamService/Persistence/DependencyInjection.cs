using Shared.Infrastructure.Persistence.Extensions;

namespace TeamService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRelationalDatabase<TeamContext>(configuration.GetConnectionString("Database")!);

        return services;
    }
}