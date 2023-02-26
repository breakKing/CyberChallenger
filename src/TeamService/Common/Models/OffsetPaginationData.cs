namespace TeamService.Common.Models;

public sealed record OffsetPaginationData(long ItemsCount, int PageNumber, int PageSize, int PagesCount);