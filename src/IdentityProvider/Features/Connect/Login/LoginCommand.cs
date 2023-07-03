using Mediator;

namespace IdentityProviderService.Features.Connect.Login;

public sealed record LoginCommand(string Login, string Password) : ICommand<LoginResponse>;