namespace Shared.Infrastructure.Persistence.Models.Dto;

/// <summary>
/// Пагинация по оффсету (классическая пагинация)
/// </summary>
public sealed class DbOffsetPagination
{
    public long TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public DbOffsetPagination(long totalCount, int pageNumber, int pageSize)
    {
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}