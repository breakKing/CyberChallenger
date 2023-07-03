namespace Common.Presentation.Primitives;

public sealed record PaginatedData<TItem>(List<TItem> Items, PaginationResponse Pagination);