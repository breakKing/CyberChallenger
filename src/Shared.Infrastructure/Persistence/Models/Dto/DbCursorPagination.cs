namespace Shared.Infrastructure.Persistence.Models.Dto;

/// <summary>
/// Пагинация по курсору
/// </summary>
public sealed class DbCursorPagination
{
    public string NextCursor { get; set; }
    public bool MoreDataAvailable { get; set; }

    public DbCursorPagination(string nextCursor, bool moreDataAvailable)
    {
        NextCursor = nextCursor;
        MoreDataAvailable = moreDataAvailable;
    }
}