using Shared.Infrastructure.RelationalDatabase.Entities;

namespace TeamService.Persistence.Entities;

public sealed class TeamRole : EntityBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    internal TeamRole(int id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <inheritdoc />
    public TeamRole(string name)
    {
        Name = name;
    }
}