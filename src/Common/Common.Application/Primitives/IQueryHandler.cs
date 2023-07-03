using LanguageExt.Common;
using MediatR;

namespace Common.Application.Primitives;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : IQuery<TResponse>
{
    
}