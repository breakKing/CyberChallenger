using System.Security.Claims;
using Mediator;

namespace IdentityProviderService.Features.Connect.UserInfo;

public sealed record GetUserInfoQuery(ClaimsPrincipal? Principal) : IQuery<GetUserInfoResponse>;