using System.Security.Claims;
using Mediator;

namespace IdentityProviderService.Features.Connect.Refresh;

public sealed record RefreshCommand(ClaimsPrincipal Principal) : ICommand<RefreshResponse>;