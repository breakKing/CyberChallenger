namespace Shared.Contracts.GatewayApi.Base;

public record ApiPaginatedResponse<TData>(List<TData>? Data, PaginationResponse? Pagination, bool Failed, List<string>? 
Errors) 
    : ApiResponse<List<TData>>(Data, Failed, Errors);