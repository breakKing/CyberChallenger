using Mediator;

namespace IdentityProviderService.Features.Identity.UserRegister;

public sealed record UserRegisterCommand(string Username, string Email, string Password, string PasswordRepeat, 
    string UserAgentFingerprint) : ICommand<UserRegisterResponse>;