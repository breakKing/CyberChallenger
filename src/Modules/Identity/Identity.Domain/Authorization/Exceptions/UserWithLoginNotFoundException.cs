namespace Identity.Domain.Authorization.Exceptions;

public sealed class UserWithLoginNotFoundException : Exception
{
    public string Login { get; private set; }
    
    public UserWithLoginNotFoundException(string login) : 
        base($"User with login '{login}' was not found")
    {
        Login = login;
    }
}