namespace IdentityProvider.Features.Connect.UserInfo;

public sealed record UserInfoDto(string Id, string UserName, List<string> Roles);