namespace IdentityProviderService.Common.Constants;

public static class RoleNames
{
    /// <summary>
    /// Роль суперадмина
    /// </summary>
    public const string SuperAdmin = "superadmin";
    
    /// <summary>
    /// Роль админа
    /// </summary>
    public const string Admin = "admin";
    
    /// <summary>
    /// Роль турнирного оператора
    /// </summary>
    public const string TournamentOperator = "tournament_operator";
    
    /// <summary>
    /// Роль игрока
    /// </summary>
    public const string Player = "player";
    
    /// <summary>
    /// Роль, получаемая при регистрации (до создания профиля игрока или турнирного оператора)
    /// </summary>
    public const string Common = "common";
}