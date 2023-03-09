using KafkaFlow.TypedHandler;

namespace Shared.Infrastructure.EventSourcing.Kafka.Configuration.Interfaces;

public interface IKafkaConsumerConfigBuilder
{
    /// <summary>
    /// Установка имени консумера
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IKafkaConsumerConfigBuilder SetName(string name);
    
    /// <summary>
    /// Установка группы консумера
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    IKafkaConsumerConfigBuilder SetGroup(string group);
    
    /// <summary>
    /// Указание прослушиваемых топиков
    /// </summary>
    /// <param name="topics"></param>
    /// <returns></returns>
    IKafkaConsumerConfigBuilder SubscribeToTopics(params string[] topics);
    
    /// <summary>
    /// Сохранение порядка сообщений
    /// </summary>
    /// <param name="preserveMessageOrder"></param>
    /// <returns></returns>
    IKafkaConsumerConfigBuilder PreserveMessageOrder(bool preserveMessageOrder = true);
    
    /// <summary>
    /// Сколько воркеров (потоков) использовать
    /// </summary>
    /// <param name="workersCount"></param>
    /// <returns></returns>
    IKafkaConsumerConfigBuilder SetWorkersCount(int workersCount);

    /// <summary>
    /// Добавление обработчика сообщений
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    /// <returns></returns>
    IKafkaConsumerConfigBuilder UseHandler<THandler>()
        where THandler : IMessageHandler;
}