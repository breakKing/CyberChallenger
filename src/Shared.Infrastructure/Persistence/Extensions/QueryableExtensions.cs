using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<List<TEntity>> ToOffsetPaginatedListAsync<TEntity>(this IQueryable<TEntity> query,
        int pageNumber = 1, int pageSize = 20, CancellationToken ct = default)
    {
        return await query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }
    
    public static async Task<List<TEntity>> ToCursorPaginatedListAsync<TEntity>(this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>> cursorFilter, int pageSize = 20, CancellationToken ct = default)
    {
        // Берём на один элемент больше, чтобы знать, закончились ли данные или нет
        return await query.Where(cursorFilter)
            .Take(pageSize + 1)
            .ToListAsync(ct);
    }
}