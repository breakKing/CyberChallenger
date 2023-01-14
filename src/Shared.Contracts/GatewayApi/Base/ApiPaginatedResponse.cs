namespace Shared.Contracts.GatewayApi.Base;

public record ApiPaginatedResponse<TData>(List<TData> Data, PaginationResponse Pagination, bool Failed, string Error) 
    : ApiResponse<List<TData>>(Data, Failed, Error);