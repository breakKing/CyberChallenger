namespace Shared.Infrastructure.EventSourcing.Kafka.Configuration.Interfaces;

public interface IKafkaConfigBuilder
{
    /// <summary>
    /// Задание адресов брокеров
    /// </summary>
    /// <param name="brokerAddresses">Адреса для брокеров из кластера (адреса и порты)</param>
    /// <returns></returns>
    IKafkaConfigBuilder UseBrokers(params string[] brokerAddresses);

    /// <summary>
    /// Добавление Schema Registry
    /// </summary>
    /// <param name="schemaRegistry">Адрес Schema Registry с указанием протокола (http и https)</param>
    /// <returns></returns>
    IKafkaConfigBuilder UseSchemaRegistry(string schemaRegistry);
    
    /// <summary>
    /// Регистрация консумера с нужным конфигом
    /// <param name="configAction"></param>
    /// </summary>
    /// <returns></returns>
    IKafkaConfigBuilder RegisterConsumer(Action<IKafkaConsumerConfigBuilder> configAction);
    
    /// <summary>
    /// Регистрация продюсера с нужным конфигом
    /// </summary>
    /// <param name="configAction"></param>
    /// <returns></returns>
    IKafkaConfigBuilder RegisterProducer(Action<IKafkaProducerConfigBuilder> configAction);
}