using Mediator;

namespace IdentityProviderService.Features.Identity.UserRegister;

public sealed record UserRegisterCommand(string UserName, string Email, string Password, string PasswordRepeat, 
    string UserAgentFingerprint) : ICommand<UserRegisterResponse>;