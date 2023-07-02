namespace Identity.Application.Authorization;

public sealed record UserInfoDto(string Id, string UserName, List<string> Roles);