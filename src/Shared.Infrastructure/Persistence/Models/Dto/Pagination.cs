namespace Shared.Infrastructure.Persistence.Models.Dto;

/// <summary>
/// Пагинация для получения пагинированных данных
/// </summary>
public class Pagination
{
    public long TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public Pagination(long totalCount, int pageNumber, int pageSize)
    {
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}