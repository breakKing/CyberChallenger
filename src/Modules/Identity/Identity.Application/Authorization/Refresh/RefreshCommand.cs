using System.Security.Claims;
using Common.Application.Primitives;

namespace Identity.Application.Authorization.Refresh;

public sealed record RefreshCommand(ClaimsPrincipal Principal) : ICommand<RefreshResponse>;