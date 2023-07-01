namespace Common.Presentation.Contracts;

public record PaginationRequest(int PageNumber = 1, int PageSize = 20);