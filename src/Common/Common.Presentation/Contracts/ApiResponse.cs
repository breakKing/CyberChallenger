namespace Common.Presentation.Contracts;

public record ApiResponse<TData>(TData? Data, bool Failed, List<string>? Errors);