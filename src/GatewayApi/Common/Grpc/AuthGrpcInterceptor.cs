using FastEndpoints.Security;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Shared.Contracts.Common;

namespace GatewayApi.Common.Grpc;

public sealed class AuthGrpcInterceptor : Interceptor
{
    private readonly HttpContext? _httpContext;

    /// <inheritdoc />
    public AuthGrpcInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    /// <inheritdoc />
    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var userId = _httpContext?.User.ClaimValue(GlobalConstants.UserIdInternalHeader);
        var updatedContext = UpdateHeaders(context, userId);
        
        return base.AsyncUnaryCall(request, updatedContext, continuation);
    }

    private static ClientInterceptorContext<TRequest,TResponse> UpdateHeaders<TRequest, TResponse>(
        ClientInterceptorContext<TRequest,TResponse> context, string? userId) 
        where TRequest : class 
        where TResponse : class
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return context;
        }
        
        var headers = new Metadata { new(GlobalConstants.UserIdInternalHeader, userId) };
        var newOptions = context.Options.WithHeaders(headers);
        var newContext = new ClientInterceptorContext<TRequest, TResponse>(
            context.Method,
            context.Host,
            newOptions);

        return newContext;
    }
}