using Shared.Infrastructure.EventSourcing.Kafka.Extensions;
using TournamentService.EventSourcing.Handlers;

namespace TournamentService.EventSourcing;

public static class DependencyInjection
{
    public static IServiceCollection AddEventSourcing(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventSourcingWithKafka(config =>
        {
            config.UseBrokers("kafka:9092");
            config.UseSchemaRegistry("http://localhost:8081");
            config.DefineTopic("team", 5, 1);

            config.RegisterConsumer(consumer =>
            {
                consumer.SetName("tournament_consumer");
                consumer.SetGroup("tournament_consumer_group");
                consumer.SubscribeToTopics("team");
                consumer.PreserveMessageOrder();
                consumer.SetWorkersCount(5);
                
                consumer.UseHandler<TeamCreatedHandlerV1>();
            });
        });
        
        return services;
    }
}