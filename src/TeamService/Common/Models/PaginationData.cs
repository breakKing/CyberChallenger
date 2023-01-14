namespace TeamService.Common.Models;

public sealed record PaginationData(long ItemsCount, int PageNumber, int PageSize, int PagesCount);