using KafkaFlow;
using KafkaFlow.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Shared.Infrastructure.EventSourcing.Jobs;
using Shared.Infrastructure.EventSourcing.Kafka.Configuration.Interfaces;
using Shared.Infrastructure.EventSourcing.Services.Implementations;
using Shared.Infrastructure.EventSourcing.Services.Interfaces;

namespace Shared.Infrastructure.EventSourcing.Kafka.Configuration.Implementations;

internal sealed class KafkaFlowConfigBuilder : IKafkaConfigBuilder, IConfigBuilder<Action<IServiceCollection>>
{
    private readonly List<string> _brokers = new();
    private string? _schemaRegistry;
    private readonly List<Action<IClusterConfigurationBuilder>> _configActions = new();
    private readonly Dictionary<string, (int partitionCount, short replicationFactor)> _topics = new();

    private bool _shouldRegisterProducerAdditionalServices;

    /// <inheritdoc />
    public IKafkaConfigBuilder UseBrokers(params string[] brokerAddresses)
    {
        _brokers.AddRange(brokerAddresses);
        return this;
    }

    /// <inheritdoc />
    public IKafkaConfigBuilder UseSchemaRegistry(string schemaRegistry)
    {
        _schemaRegistry = schemaRegistry;
        return this;
    }

    /// <inheritdoc />
    public IKafkaConfigBuilder DefineTopic(string topicName, int partitionCount, short replicationFactor)
    {
        if (partitionCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(partitionCount), "Partition count must be at least 1");
        }

        if (replicationFactor <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(replicationFactor), "Replication factor must be at least 1");
        }

        if (_topics.ContainsKey(topicName))
        {
            _topics[topicName] = new ValueTuple<int, short>(partitionCount, replicationFactor);
        }
        else
        {
            _topics.Add(topicName, new ValueTuple<int, short>(partitionCount, replicationFactor));
        }
        
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
        _shouldRegisterProducerAdditionalServices = true;
        
        return this;
    }

    /// <inheritdoc />
    public Action<IServiceCollection> Build()
    {
        if (_brokers.Count == 0)
        {
            throw new InvalidOperationException("No brokers were specified");
        }

        Action<IClusterConfigurationBuilder> configAction = config =>
        {
            config.WithBrokers(_brokers.Distinct());

            if (!string.IsNullOrWhiteSpace(_schemaRegistry))
            {
                config.WithSchemaRegistry(sr =>
                {
                    sr.Url = _schemaRegistry;
                });
            }

            foreach (var topic in _topics)
            {
                config.CreateTopicIfNotExists(topic.Key, topic.Value.partitionCount, topic.Value.replicationFactor);
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

            if (_shouldRegisterProducerAdditionalServices)
            {
                AddProducerJobToServices(services);
                services.AddScoped<IEventProducer, EventProducer>();
            }
        };

        return servicesConfig;
    }

    private static IServiceCollection AddProducerJobToServices(IServiceCollection services)
    {
        services.AddQuartz(config =>
        {
            config.UseMicrosoftDependencyInjectionJobFactory();
            
            config.AddJob<ProduceMessagesFromOutboxJob>(job =>
            {
                job.DisallowConcurrentExecution();
                job.WithIdentity("outbox_producer");
            });

            config.AddTrigger(trigger =>
            {
                trigger.ForJob("outbox_producer");
                trigger.WithIdentity("outbox_producer_trigger");
                
                trigger.StartNow();
                trigger.WithSimpleSchedule(sc => sc.WithIntervalInMinutes(1).RepeatForever());
            });
        });
        
        services.AddQuartzHostedService(c => c.WaitForJobsToComplete = true);

        return services;
    }
}