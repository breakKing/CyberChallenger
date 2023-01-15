using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Shared.Contracts.Common;
using Shared.Grpc.Interfaces;

namespace Shared.Grpc.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly HttpContext _httpContext;

    public CurrentUserService(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    /// <inheritdoc />
    public Guid? GetIdFromHttpContext()
    {
        var result =
            _httpContext.Request.Headers.TryGetValue(GlobalConstants.UserIdHeader, out var userId);
        
        if (!result)
        {
            return new Guid(userId.ToString());
        }

        return null;
    }

    /// <inheritdoc />
    public Guid? GetIdFromGrpcContext(ServerCallContext context)
    {
        var userId = context.RequestHeaders.FirstOrDefault(e => e.Key == GlobalConstants.UserIdHeader)?.Value ?? null;
        
        if (!string.IsNullOrEmpty(userId))
        {
            return new Guid(userId);
        }

        return null;
    }
    
    
}