using LanguageExt.Common;
using MediatR;

namespace Common.Application.Handling;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
    
}

public interface ICommand : ICommand<bool>
{
    
}