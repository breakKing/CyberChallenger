namespace Shared.Infrastructure.RelationalDatabase.Models.Dto;

/// <summary>
/// Данные, полученные с классической пагинацией (по оффсету)
/// </summary>
/// <typeparam name="TData"></typeparam>
public sealed class DbOffsetPaginatedData<TData>
{
    public List<TData> Data { get; set; }
    public DbOffsetPagination Pagination { get; set; }

    public DbOffsetPaginatedData(List<TData> data, DbOffsetPagination pagination)
    {
        Data = data;
        Pagination = pagination;
    }
}