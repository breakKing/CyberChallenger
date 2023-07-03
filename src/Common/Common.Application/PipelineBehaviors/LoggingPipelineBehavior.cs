using Common.Application.Primitives;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Application.PipelineBehaviors;

public sealed class LoggingPipelineBehavior<TRequest, TResponse> : 
    IApplicationPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<Result<TResponse>> Handle(
        TRequest request, 
        RequestHandlerDelegate<Result<TResponse>> next, 
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("The processing of a request {@Request} has been started", request);
        var result = await next();
        _logger.LogInformation("The processing of a request {@Request} has been finished", request);

        return result;
    }
}