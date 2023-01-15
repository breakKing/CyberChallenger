using Mediator;

namespace IdentityProviderService.Features.Identity.UserLogin;

public sealed record UserLoginCommand(string UsernameOrEmail, string Password, string UserAgentFingerprint) : 
        ICommand<UserLoginResponse>;