using KafkaFlow;

namespace Shared.Infrastructure.EventSourcing.Kafka.Middlewares;

public sealed class InboxKafkaMiddleware : IMessageMiddleware
{
    /// <inheritdoc />
    public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
    }
}