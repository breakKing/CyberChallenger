using System.Text.Json.Serialization;

namespace Shared.Contracts.GatewayApi.Base;

public sealed record PaginationRequest(
    [property: JsonPropertyName("pageNumber")] int PageNumber, 
    [property: JsonPropertyName("pageSize")] int PageSize);