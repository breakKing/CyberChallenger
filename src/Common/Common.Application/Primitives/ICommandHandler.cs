using LanguageExt.Common;
using MediatR;

namespace Common.Application.Primitives;

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : ICommand<TResponse>
{
    
}

public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, Result<bool>>
    where TRequest : ICommand
{
    
}