using Shared.Infrastructure.Persistence.Extensions;

namespace TournamentService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRelationalDatabase<TournamentContext>(
            configuration.GetConnectionString("Database")!);

        return services;
    }
}