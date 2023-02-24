using System.Net;
using Shared.Contracts.GatewayApi.Base;

namespace GatewayApi.Endpoints;

public abstract class EndpointBase<TRequest, TResponse> : Endpoint<TRequest, ApiResponse<TResponse>>
    where TRequest : notnull, new()
{
    protected async Task SendDataAsync(TResponse response, HttpStatusCode statusCode = HttpStatusCode.OK,
        CancellationToken ct = default)
    {
        var apiResponse = new ApiResponse<TResponse>(response, false, null);
        await SendAsync(apiResponse, (int)statusCode, cancellation: ct);
    }

    protected async Task SendErrorsAsync(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        CancellationToken ct = default)
    {
        var apiResponse = new ApiResponse<TResponse>(default, true, errors);
        await SendAsync(apiResponse, (int)statusCode, ct);
    }

    protected async Task SendErrorsAsync(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        CancellationToken ct = default)
    {
        await SendErrorsAsync(new List<string> { error }, statusCode, ct);
    }


    protected virtual void ConfigureSwaggerDescription(EndpointSummaryBase summary, params HttpStatusCode[] statusCodes)
    {
        Description(desc =>
        {
            desc.Accepts<TRequest>("application/json");

            foreach (var code in statusCodes)
            {
                desc.Produces<ApiResponse<TResponse>>((int)code);
            }
        }, clearDefaults: true);

        Summary(summary);
    }
}

public abstract class EndpointWithoutRequestBase<TResponse> : EndpointBase<EmptyRequest, TResponse>
{
    protected override void ConfigureSwaggerDescription(EndpointSummaryBase summary,
        params HttpStatusCode[] statusCodes)
    {
        Description(desc =>
        {
            foreach (var code in statusCodes)
            {
                desc.Produces<ApiResponse<TResponse>>((int)code);
            }
        }, clearDefaults: true);

        Summary(summary);
    }
}