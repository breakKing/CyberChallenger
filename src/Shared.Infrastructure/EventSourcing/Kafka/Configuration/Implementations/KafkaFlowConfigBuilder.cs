using KafkaFlow;
using KafkaFlow.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EventSourcing.Kafka.Configuration.Interfaces;

namespace Shared.Infrastructure.EventSourcing.Kafka.Configuration.Implementations;

internal sealed class KafkaFlowConfigBuilder : IKafkaConfigBuilder, IConfigBuilder<Action<IServiceCollection>>
{
    private string[] _brokers = Array.Empty<string>();
    private string? _schemaRegistry;
    private List<Action<IClusterConfigurationBuilder>> _configActions = new();

    /// <inheritdoc />
    public IKafkaConfigBuilder UseBrokers(params string[] brokerAddresses)
    {
        _brokers = brokerAddresses;
        return this;
    }

    /// <inheritdoc />
    public IKafkaConfigBuilder UseSchemaRegistry(string schemaRegistry)
    {
        _schemaRegistry = schemaRegistry;
        return this;
    }

    /// <inheritdoc />
    public IKafkaConfigBuilder RegisterConsumer(Action<IKafkaConsumerConfigBuilder> configAction)
    {
        var consumerBuilder = new KafkaFlowConsumerConfigBuilder();
        
        configAction.Invoke(consumerBuilder);
        var additionalAction = consumerBuilder.Build();
        
        _configActions.Add(additionalAction);

        return this;
    }

    /// <inheritdoc />
    public IKafkaConfigBuilder RegisterProducer(Action<IKafkaProducerConfigBuilder> configAction)
    {
        var producerBuilder = new KafkaFlowProducerConfigBuilder();
        
        configAction.Invoke(producerBuilder);
        var additionalAction = producerBuilder.Build();
        
        _configActions.Add(additionalAction);
        
        return this;
    }

    /// <inheritdoc />
    public Action<IServiceCollection> Build()
    {
        if (_brokers.Length == 0)
        {
            throw new InvalidOperationException("No brokers were specified");
        }

        Action<IClusterConfigurationBuilder> configAction = config =>
        {
            config.WithBrokers(_brokers);

            if (!string.IsNullOrWhiteSpace(_schemaRegistry))
            {
                config.WithSchemaRegistry(sr =>
                {
                    sr.Url = _schemaRegistry;
                });
            }

            foreach (var action in _configActions)
            {
                action.Invoke(config);
            }
        };

        Action<IServiceCollection> servicesConfig = services =>
        {
            services.AddKafka(kafka =>
            {
                kafka.UseMicrosoftLog();
                kafka.AddCluster(configAction);
            });
        };

        return servicesConfig;
    }
}