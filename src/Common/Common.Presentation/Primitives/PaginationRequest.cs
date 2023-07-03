namespace Common.Presentation.Primitives;

public record PaginationRequest(int PageNumber = 1, int PageSize = 20);