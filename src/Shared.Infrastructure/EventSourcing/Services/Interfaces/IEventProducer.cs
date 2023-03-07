namespace Shared.Infrastructure.EventSourcing.Services.Interfaces;

public interface IEventProducer
{
    /// <summary>
    /// Отправка события в виде сообщения в Outbox для дальнейшей отправки в топик
    /// </summary>
    /// <param name="producerName">Имя продюсера</param>
    /// <param name="topic">Топик дял отправки</param>
    /// <param name="key">Ключ сообщения (для распределения в нужный partition)</param>
    /// <param name="message">Событие в виде сообщения</param>
    /// <param name="headers"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TMessage">Тип сообщения</typeparam>
    /// <returns></returns>
    Task ProduceAsync<TMessage>(string producerName, string topic, string key, TMessage message, 
        IDictionary<string, byte[]>? headers = null, CancellationToken ct = default);
}