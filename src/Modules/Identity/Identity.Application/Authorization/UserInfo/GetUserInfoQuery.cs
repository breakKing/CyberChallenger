using System.Security.Claims;
using Common.Application.Primitives;

namespace Identity.Application.Authorization.UserInfo;

public sealed record GetUserInfoQuery(ClaimsPrincipal? Principal) : IQuery<GetUserInfoResponse>;