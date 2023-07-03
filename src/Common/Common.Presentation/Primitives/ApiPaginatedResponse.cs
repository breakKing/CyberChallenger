namespace Common.Presentation.Primitives;

public record ApiPaginatedResponse<TData>(
        PaginatedData<TData> Data,
        bool Failed,
        List<string>? Errors)
    : ApiResponse<PaginatedData<TData>>(Data, Failed, Errors);