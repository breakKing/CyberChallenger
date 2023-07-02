using Common.Application.Primitives;

namespace Identity.Application.Authorization.Login;

public sealed record LoginCommand(string Login, string Password) : ICommand<LoginResponse>;