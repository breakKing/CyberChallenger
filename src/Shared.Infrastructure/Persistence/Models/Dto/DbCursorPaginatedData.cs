namespace Shared.Infrastructure.Persistence.Models.Dto;

/// <summary>
/// Данные, полученные с пагинацией по курсору
/// </summary>
/// <typeparam name="TData"></typeparam>
public sealed class DbCursorPaginatedData<TData>
{
    public List<TData> Data { get; set; }
    public DbCursorPagination Pagination { get; set; }

    public DbCursorPaginatedData(List<TData> data, DbCursorPagination pagination)
    {
        Data = data;
        Pagination = pagination;
    }
}