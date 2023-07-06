namespace Common.Presentation.Primitives;

public sealed record PagedList<TItem>(List<TItem> Items, PaginationResponse Pagination);