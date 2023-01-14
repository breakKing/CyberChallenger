using TeamService.Persistence.Entities;

namespace TeamService.Persistence.Constants;

public static class TeamRolesDefinitions
{
    public static readonly TeamRole Player = new TeamRole(1, "Игрок");
    public static readonly TeamRole Captain = new TeamRole(2, "Капитан");
    public static readonly TeamRole Coach = new TeamRole(3, "Тренер");
    public static readonly TeamRole Manager = new TeamRole(4, "Менеджер");
    
    public static readonly List<TeamRole> FullList = new()
    {
        Player,
        Captain,
        Coach,
        Manager
    };
}