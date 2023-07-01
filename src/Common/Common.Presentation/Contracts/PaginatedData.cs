namespace Common.Presentation.Contracts;

public sealed record PaginatedData<TItem>(List<TItem> Items, PaginationResponse Pagination);