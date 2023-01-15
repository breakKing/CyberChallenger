using Grpc.Core;

namespace Shared.Grpc.Interfaces;

public interface ICurrentUserService
{
    Guid? GetIdFromHttpContext();
    Guid? GetIdFromGrpcContext(ServerCallContext context);
}