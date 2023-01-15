using Mediator;

namespace IdentityProviderService.Features.Identity.UserLogout;

public sealed record UserLogoutCommand(string UserAgentFingerprint) : ICommand<UserLogoutResponse>;