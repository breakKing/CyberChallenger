using Grpc.Core;
using Shared.Grpc.Interfaces;

namespace Shared.Grpc.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    /// <inheritdoc />
    public Guid? GetIdFromGrpcContext(ServerCallContext context)
    {
        var userId = context.RequestHeaders.FirstOrDefault(e => e.Key == "UserId")?.Value ?? null;
        
        if (!string.IsNullOrEmpty(userId))
        {
            return new Guid(userId);
        }

        return null;
    }
}