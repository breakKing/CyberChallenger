using System.Security.Claims;
using Mediator;

namespace IdentityProvider.Features.Connect.Refresh;

public sealed record RefreshCommand(ClaimsPrincipal Principal) : ICommand<RefreshResponse>;