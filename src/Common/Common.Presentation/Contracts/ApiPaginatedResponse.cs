namespace Common.Presentation.Contracts;

public record ApiPaginatedResponse<TData>(
        PaginatedData<TData> Data,
        bool Failed,
        List<string>? Errors)
    : ApiResponse<PaginatedData<TData>>(Data, Failed, Errors);