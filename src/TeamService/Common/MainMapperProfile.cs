using Mapster;
using Shared.Contracts.Common;
using TeamService.Common.Models;

namespace TeamService.Common;

public sealed class MainMapperProfile : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OffsetPaginationGrpcRequest, OffsetPagination>();
        config.NewConfig<OffsetPaginationData, OffsetPaginationGrpcResponse>();
        
        config.NewConfig<CursorPaginationGrpcRequest, CursorPagination>();
        config.NewConfig<CursorPaginationData, CursorPaginationGrpcResponse>();
    }
}