using LanguageExt.Common;
using MediatR;

namespace Common.Application.Primitives;

public interface IApplicationPipelineBehavior<in TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>> 
    where TRequest : notnull
{
    
}