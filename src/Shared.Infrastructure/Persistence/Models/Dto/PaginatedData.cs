namespace Shared.Infrastructure.Persistence.Models.Dto;

/// <summary>
/// Данные, полученные с пагинацией
/// </summary>
/// <typeparam name="TData"></typeparam>
public class PaginatedData<TData>
{
    public List<TData> Data { get; set; }
    public Pagination Pagination { get; set; }

    public PaginatedData(List<TData> data, Pagination pagination)
    {
        Data = data;
        Pagination = pagination;
    }
}