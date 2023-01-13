using KafkaFlow;
using KafkaFlow.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure.Kafka.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafkaEventLogging(this IServiceCollection services,
        Action<IClusterConfigurationBuilder> builderAction)
    {
        services.AddKafka(kafka =>
        {
            kafka.UseMicrosoftLog();
            kafka.AddCluster(builderAction);
        });
        
        return services;
    }
}