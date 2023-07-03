namespace Common.Application.Exceptions;

public sealed class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; private set; }

    /// <inheritdoc />
    public ValidationException(IDictionary<string, string[]> errors) : 
        base("There were one or more validation errors")
    {
        Errors = errors;
    }
}