namespace Shared.Infrastructure.Persistence.Models.Dto;

/// <summary>
/// Пагинация для получения пагинированных данных
/// </summary>
public class DbPagination
{
    public long TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public DbPagination(long totalCount, int pageNumber, int pageSize)
    {
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}