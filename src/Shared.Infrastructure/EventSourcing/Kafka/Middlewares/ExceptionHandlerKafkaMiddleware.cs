using KafkaFlow;
using Microsoft.Extensions.Logging;

namespace Shared.Infrastructure.EventSourcing.Kafka.Middlewares;

public sealed class ExceptionHandlerKafkaMiddleware : IMessageMiddleware
{
    private readonly ILogger<ExceptionHandlerKafkaMiddleware> _logger;

    public ExceptionHandlerKafkaMiddleware(ILogger<ExceptionHandlerKafkaMiddleware> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        try
        {
            await next(context);
            context.ConsumerContext?.StoreOffset();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occurred while processing the message {@Message}", context);
        }
    }
}