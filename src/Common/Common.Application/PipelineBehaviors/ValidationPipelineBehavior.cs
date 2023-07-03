using Common.Application.Primitives;
using FluentValidation;
using LanguageExt.Common;
using MediatR;
using ValidationException = Common.Application.Exceptions.ValidationException;

namespace Common.Application.PipelineBehaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse> :
    IApplicationPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <inheritdoc />
    public async Task<Result<TResponse>> Handle(
        TRequest request, 
        RequestHandlerDelegate<Result<TResponse>> next, 
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var context = new ValidationContext<TRequest>(request);
        
        var errorsDictionary = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);
        
        if (errorsDictionary.Any())
        {
            var exception = new ValidationException(errorsDictionary);
            return new Result<TResponse>(exception);
        }
        
        return await next();
    }
}