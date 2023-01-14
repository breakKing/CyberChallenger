namespace Shared.Contracts.GatewayApi.Base;

public sealed record PaginatedData<TItem>(List<TItem> Items, PaginationResponse Pagination);