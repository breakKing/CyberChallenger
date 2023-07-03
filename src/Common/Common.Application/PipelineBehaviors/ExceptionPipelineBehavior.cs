using Common.Application.Primitives;
using LanguageExt.Common;
using MediatR;

namespace Common.Application.PipelineBehaviors;

public sealed class ExceptionPipelineBehavior<TRequest, TResponse> : 
    IApplicationPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    /// <inheritdoc />
    public async Task<Result<TResponse>> Handle(
        TRequest request, 
        RequestHandlerDelegate<Result<TResponse>> next, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            return new Result<TResponse>(e);
        }
    }
}