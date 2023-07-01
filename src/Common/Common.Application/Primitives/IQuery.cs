using LanguageExt.Common;
using MediatR;

namespace Common.Application.Primitives;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}