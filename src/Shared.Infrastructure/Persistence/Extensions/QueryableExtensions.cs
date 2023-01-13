using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<List<TEntity>> ToListFromPaginationAsync<TEntity>(this IQueryable<TEntity> query,
        int pageNumber = 1, int pageSize = 20, CancellationToken ct = default)
    {
        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
    }
}