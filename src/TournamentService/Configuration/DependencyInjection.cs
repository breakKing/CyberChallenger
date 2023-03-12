using TournamentService.EventSourcing;
using TournamentService.Persistence;

namespace TournamentService.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddEventSourcing(configuration);
        
        return services;
    }
}