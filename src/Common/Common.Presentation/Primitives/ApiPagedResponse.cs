namespace Common.Presentation.Primitives;

public record ApiPagedResponse<TData>(
        PagedList<TData> Data,
        bool Failed,
        List<string>? Errors)
    : ApiResponse<PagedList<TData>>(Data, Failed, Errors);