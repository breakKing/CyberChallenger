namespace Shared.Contracts.GatewayApi.Base;

public sealed record PaginationResponse(long ItemsCount, int PageNumber, int PageSize, int PagesCount);