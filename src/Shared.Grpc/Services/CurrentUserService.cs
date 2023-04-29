using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Shared.Contracts.Common;
using Shared.Grpc.Interfaces;

namespace Shared.Grpc.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly HttpContext? _httpContext;

    public CurrentUserService(IHttpContextAccessor accessor)
    {
        _httpContext = accessor.HttpContext;
    }

    /// <inheritdoc />
    public Guid? GetIdFromHttpContext()
    {
        if (_httpContext?.Request.Headers.ContainsKey(GlobalConstants.UserIdInternalHeader) ?? false)
        {
            return Guid.Parse(_httpContext.Request.Headers[GlobalConstants.UserIdInternalHeader]!);
        }

        return null;
    }

    /// <inheritdoc />
    public Guid? GetIdFromGrpcContext(ServerCallContext context)
    {
        var userId = context.RequestHeaders.FirstOrDefault(e => e.Key == GlobalConstants.UserIdInternalHeader)?.Value;
        
        if (!string.IsNullOrEmpty(userId))
        {
            return new Guid(userId);
        }

        return null;
    }
    
    
}