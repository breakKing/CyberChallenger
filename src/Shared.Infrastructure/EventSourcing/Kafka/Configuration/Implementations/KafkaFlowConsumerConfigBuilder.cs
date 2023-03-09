using KafkaFlow;
using KafkaFlow.Configuration;
using KafkaFlow.Consumers.DistributionStrategies;
using KafkaFlow.Serializer.SchemaRegistry;
using KafkaFlow.TypedHandler;
using Shared.Infrastructure.EventSourcing.Kafka.Configuration.Interfaces;
using Shared.Infrastructure.EventSourcing.Kafka.Middlewares;
using Shared.Infrastructure.EventSourcing.Kafka.Resolvers;

namespace Shared.Infrastructure.EventSourcing.Kafka.Configuration.Implementations;

public sealed class KafkaFlowConsumerConfigBuilder : IKafkaConsumerConfigBuilder,
    IConfigBuilder<Action<IClusterConfigurationBuilder>>
{
    private string? _name;
    private string? _group;
    private string[] _topics = Array.Empty<string>();
    private bool _preserveMessageOrder = true;
    private int _workersCount = 1;
    private List<Type> _handlers = new();
    
    /// <inheritdoc />
    public IKafkaConsumerConfigBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    /// <inheritdoc />
    public IKafkaConsumerConfigBuilder SetGroup(string group)
    {
        _group = group;
        return this;
    }

    /// <inheritdoc />
    public IKafkaConsumerConfigBuilder SubscribeToTopics(params string[] topics)
    {
        _topics = topics;
        return this;
    }

    /// <inheritdoc />
    public IKafkaConsumerConfigBuilder PreserveMessageOrder(bool preserveMessageOrder = true)
    {
        _preserveMessageOrder = preserveMessageOrder;
        return this;
    }

    /// <inheritdoc />
    public IKafkaConsumerConfigBuilder SetWorkersCount(int workersCount)
    {
        _workersCount = workersCount;
        return this;
    }

    /// <inheritdoc />
    public IKafkaConsumerConfigBuilder UseHandler<THandler>() where THandler : IMessageHandler
    {
        _handlers.Add(typeof(THandler));
        return this;
    }

    /// <inheritdoc />
    public Action<IClusterConfigurationBuilder> Build()
    {
        if (_name is null)
        {
            throw new InvalidOperationException("Consumer name was not specified");
        }

        if (_topics.Length == 0)
        {
            throw new InvalidOperationException("No topics were specified");
        }

        if (_workersCount <= 0)
        {
            throw new InvalidOperationException($"Invalid number of workers ({_workersCount}) was specified");
        }

        Action<IClusterConfigurationBuilder> configAction = cluster =>
        {
            cluster.AddConsumer(consumer =>
            {
                consumer.WithName(_name);
                consumer.Topics(_topics);
                consumer.WithWorkersCount(_workersCount);

                if (!string.IsNullOrWhiteSpace(_group))
                {
                    consumer.WithGroupId(_group);
                }

                if (_preserveMessageOrder)
                {
                    consumer.WithWorkDistributionStrategy<BytesSumDistributionStrategy>();
                }
                else
                {
                    consumer.WithWorkDistributionStrategy<FreeWorkerDistributionStrategy>();
                }

                consumer.AddMiddlewares(mw =>
                {
                    mw.AddAtBeginning<ExceptionHandlerKafkaMiddleware>();
                    mw.Add<InboxKafkaMiddleware>();
                    mw.AddSerializer<ConfluentProtobufSerializer, CustomTypeKafkaResolver>();
                    mw.AddTypedHandlers(h =>
                    {
                        h.WithHandlerLifetime(InstanceLifetime.Scoped);
                        h.AddHandlers(_handlers);
                    });
                });
            });
        };

        return configAction;
    }
}