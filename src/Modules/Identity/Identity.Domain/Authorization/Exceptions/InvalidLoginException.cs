namespace Identity.Domain.Authorization.Exceptions;

public sealed class InvalidLoginException : Exception
{
    public string Login { get; private set; }
    
    public InvalidLoginException(string login) : 
        base($"The invalid credentials were provided for user with login '{login}'")
    {
        Login = login;
    }
}