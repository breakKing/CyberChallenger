namespace Shared.Infrastructure.EventSourcing.Kafka.Configuration.Interfaces;

public interface IKafkaProducerConfigBuilder
{
    /// <summary>
    /// Установка имени продюсера
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IKafkaProducerConfigBuilder SetName(string name);
    
    /// <summary>
    /// Установка топика по умолчанию
    /// </summary>
    /// <param name="topic"></param>
    /// <returns></returns>
    IKafkaProducerConfigBuilder SetDefaultTopic(string topic);
}