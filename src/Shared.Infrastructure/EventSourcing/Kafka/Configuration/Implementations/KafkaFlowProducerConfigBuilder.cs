using KafkaFlow;
using KafkaFlow.Configuration;
using KafkaFlow.Serializer.SchemaRegistry;
using Shared.Infrastructure.EventSourcing.Kafka.Configuration.Interfaces;
using Shared.Infrastructure.EventSourcing.Kafka.Middlewares;
using Shared.Infrastructure.EventSourcing.Kafka.Resolvers;

namespace Shared.Infrastructure.EventSourcing.Kafka.Configuration.Implementations;

public sealed class KafkaFlowProducerConfigBuilder : IKafkaProducerConfigBuilder,
    IConfigBuilder<Action<IClusterConfigurationBuilder>>
{
    private string? _name;
    private string? _defaultTopic;
    
    /// <inheritdoc />
    public IKafkaProducerConfigBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    /// <inheritdoc />
    public IKafkaProducerConfigBuilder SetDefaultTopic(string topic)
    {
        _defaultTopic = topic;
        return this;
    }

    /// <inheritdoc />
    public Action<IClusterConfigurationBuilder> Build()
    {
        if (_name is null)
        {
            throw new InvalidOperationException("Producer name was not specified");
        }

        Action<IClusterConfigurationBuilder> configAction = cluster =>
        {
            cluster.AddProducer(_name, producer =>
            {
                if (!string.IsNullOrWhiteSpace(_defaultTopic))
                {
                    producer.DefaultTopic(_defaultTopic);
                }

                producer.AddMiddlewares(mw =>
                {
                    mw.AddAtBeginning<ExceptionHandlerKafkaMiddleware>();
                    mw.AddSerializer<ConfluentProtobufSerializer, CustomTypeKafkaResolver>();
                });
            });
        };

        return configAction;
    }
}