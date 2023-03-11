using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EventSourcing.Kafka.Configuration.Implementations;
using Shared.Infrastructure.EventSourcing.Kafka.Configuration.Interfaces;

namespace Shared.Infrastructure.EventSourcing.Kafka.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление поддержки асинхронного обмена событиями при помощи Kafka
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddEventSourcingWithKafka(this IServiceCollection services,
        Action<IKafkaConfigBuilder> builderAction)
    {
        var kafkaBuilder = new KafkaFlowConfigBuilder();
        builderAction.Invoke(kafkaBuilder);

        var servicesConfiguratorAction = kafkaBuilder.Build();
        servicesConfiguratorAction.Invoke(services);

        return services;
    }
}