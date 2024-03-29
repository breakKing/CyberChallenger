﻿using System.Net;
using Common.Presentation.Primitives;
using FastEndpoints;

namespace Common.Presentation.Abstractions;

public abstract class EndpointSummaryBase : EndpointSummary
{
    protected void AddSuccessResponseExample<TResponse>(HttpStatusCode statusCode, TResponse response)
    {
        ResponseExamples[(int)statusCode] = new ApiResponse<TResponse>(response, false, null);
    }
    
    protected void AddFailResponseExamples(params HttpStatusCode[] statusCodes)
    {
        foreach (var code in statusCodes)
        {
            ResponseExamples[(int)code] = new ApiResponse<object>(
                null, true, new() { "Error1", "Error2", "..." });
        }
    }
}