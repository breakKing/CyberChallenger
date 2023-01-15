namespace Shared.Infrastructure.Persistence.Models.Dto;

/// <summary>
/// Данные, полученные с пагинацией
/// </summary>
/// <typeparam name="TData"></typeparam>
public class DbPaginatedData<TData>
{
    public List<TData> Data { get; set; }
    public DbPagination Pagination { get; set; }

    public DbPaginatedData(List<TData> data, DbPagination pagination)
    {
        Data = data;
        Pagination = pagination;
    }
}