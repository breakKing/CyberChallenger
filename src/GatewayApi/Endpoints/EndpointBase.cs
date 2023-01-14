using Shared.Contracts.GatewayApi.Base;

namespace GatewayApi.Endpoints;

public abstract class EndpointBase<TRequest, TResponse> : Endpoint<TRequest, ApiResponse<TResponse>>
    where TRequest : notnull, new()
{
    protected async Task SendDataAsync(TResponse response, int statusCode = 200, CancellationToken ct = default)
    {
        var apiResponse = new ApiResponse<TResponse>(response, false, null);
        await SendAsync(apiResponse, statusCode, cancellation: ct);
    }

    protected async Task SendErrorAsync(List<string> errors, int statusCode = 400, CancellationToken ct = default)
    {
        var apiResponse = new ApiResponse<TResponse>(default, true, errors);
        await SendAsync(apiResponse, statusCode, ct);
    }
}

public abstract class EndpointWithPaginationBase<TRequest, TResponse> : Endpoint<TRequest, ApiPaginatedResponse<TResponse>>
    where TRequest : notnull, new()
{
    protected async Task SendDataAsync(List<TResponse> responses, PaginationResponse pagination, int statusCode = 200, 
        CancellationToken ct = default)
    {
        var apiResponse = new ApiPaginatedResponse<TResponse>(responses, pagination, false, null);
        await SendAsync(apiResponse, statusCode, cancellation: ct);
    }

    protected async Task SendErrorAsync(List<string> errors, int statusCode = 400, CancellationToken ct = default)
    {
        var apiResponse = new ApiPaginatedResponse<TResponse>(default, null, true, errors);
        await SendAsync(apiResponse, statusCode, ct);
    }
}