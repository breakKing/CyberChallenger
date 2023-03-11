using Shared.Infrastructure.EventSourcing.Kafka.Extensions;

namespace TeamService.EventSourcing;

public static class DependencyInjection
{
    public static IServiceCollection AddEventSourcing(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventSourcingWithKafka(config =>
        {
            config.UseBrokers("localhost:9092");
            config.UseSchemaRegistry("http://localhost:8081");
            config.DefineTopic("team", 5, 1);

            config.RegisterProducer(producer =>
            {
                producer.SetName("team_producer");
            });
        });
        
        return services;
    }
}