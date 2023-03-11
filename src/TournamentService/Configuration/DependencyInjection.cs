using TournamentService.EventSourcing;

namespace TournamentService.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventSourcing(configuration);
        
        return services;
    }
}