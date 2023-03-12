using Microsoft.EntityFrameworkCore;

namespace TournamentService.Persistence;

public sealed class TournamentContext : DbContext
{
    /// <inheritdoc />
    public TournamentContext(DbContextOptions options) : base(options)
    {
    }
}