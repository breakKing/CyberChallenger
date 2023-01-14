using Grpc.Core;

namespace Shared.Grpc.Interfaces;

public interface ICurrentUserService
{
    Guid? GetIdFromGrpcContext(ServerCallContext context);
}