using LanguageExt.Common;
using MediatR;

namespace Common.Application.Handling;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}