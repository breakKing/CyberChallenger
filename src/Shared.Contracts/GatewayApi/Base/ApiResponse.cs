namespace Shared.Contracts.GatewayApi.Base;

public record ApiResponse<TData>(TData Data, bool Failed, string Error);