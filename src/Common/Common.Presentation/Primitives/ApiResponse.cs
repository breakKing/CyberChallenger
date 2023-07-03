namespace Common.Presentation.Primitives;

public record ApiResponse<TData>(TData? Data, bool Failed, List<string>? Errors);