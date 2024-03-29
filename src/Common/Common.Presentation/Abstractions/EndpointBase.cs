﻿using System.Net;
using Common.Presentation.Primitives;
using ErrorOr;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Common.Presentation.Abstractions;

public abstract class EndpointBase<TRequest, TResponse> : Endpoint<TRequest, ApiResponse<TResponse>>
    where TRequest : notnull
{
    protected async Task SendDataAsync(
        TResponse response, 
        HttpStatusCode statusCode = HttpStatusCode.OK,
        CancellationToken ct = default)
    {
        var apiResponse = new ApiResponse<TResponse>(response, false, null);
        await SendAsync(apiResponse, (int)statusCode, cancellation: ct);
    }

    protected async Task SendErrorsAsync(
        List<string> errors, 
        HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        CancellationToken ct = default)
    {
        var apiResponse = new ApiResponse<TResponse>(default, true, errors);
        await SendAsync(apiResponse, (int)statusCode, ct);
    }

    protected async Task SendErrorsAsync(
        string error, 
        HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        CancellationToken ct = default)
    {
        await SendErrorsAsync(new List<string> { error }, statusCode, ct);
    }
    
    protected async Task SendResponseAsync<TResult>(
        ErrorOr<TResult> result, 
        Func<TResult, TResponse> apiResponseBuilder,
        CancellationToken ct = default)
    {
        var task = result.Match(
            data => SendDataAsync(apiResponseBuilder.Invoke(data), ct: ct),
            errors => SendErrorsAsync(
                errors.Select(e => e.Description).ToList(),
                HttpStatusCode.BadRequest,
                ct));

        await task;
    }

    protected virtual void ConfigureSwaggerDescription(
        EndpointSummaryBase summary, 
        params HttpStatusCode[] statusCodes)
    {
        DontAutoTag();
        
        Description(desc =>
        {
            foreach (var code in statusCodes)
            {
                desc.Produces<ApiResponse<TResponse>>((int)code);
            }
        });

        Summary(summary);
    }
}

public abstract class EndpointWithPaginationBase<TRequest, TResponse> :
    EndpointBase<TRequest, PagedList<TResponse>>
    where TRequest : PaginationRequest
{
    
}

public abstract class EndpointWithoutRequestBase<TResponse> : EndpointBase<EmptyRequest, TResponse>
{

}

public abstract class EndpointWithoutResponseBase<TRequest> : EndpointBase<TRequest, EmptyResponse>
    where TRequest : notnull
{
    
}