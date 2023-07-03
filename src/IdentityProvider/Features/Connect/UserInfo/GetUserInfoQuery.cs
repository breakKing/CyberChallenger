using System.Security.Claims;
using Mediator;

namespace IdentityProvider.Features.Connect.UserInfo;

public sealed record GetUserInfoQuery(ClaimsPrincipal? Principal) : IQuery<GetUserInfoResponse>;