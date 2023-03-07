using KafkaFlow;
using KafkaFlow.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EventSourcing.Services.Implementations;
using Shared.Infrastructure.EventSourcing.Services.Interfaces;

namespace Shared.Infrastructure.EventSourcing.Kafka.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventSourcingWithKafka(this IServiceCollection services,
        Action<IClusterConfigurationBuilder> builderAction)
    {
        services.AddKafka(kafka =>
        {
            kafka.UseMicrosoftLog();
            kafka.AddCluster(builderAction);
        });

        services.AddScoped<IEventProducer, EventProducer>();
        
        return services;
    }
}