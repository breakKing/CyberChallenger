using Ardalis.Specification.EntityFrameworkCore;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence.Entities;
using Shared.Infrastructure.Persistence.Extensions;
using Shared.Infrastructure.Persistence.Interfaces;
using Shared.Infrastructure.Persistence.Models.Dto;
using Shared.Infrastructure.Persistence.Specifications;

namespace Shared.Infrastructure.Persistence.Implementations;

public sealed class EntityFrameworkCoreRepository<TEntity, TContext> : IGenericRepository<TEntity>
    where TEntity : EntityBase
    where TContext : DbContext
{
    private readonly TContext _context;
    
    private IQueryable<TEntity> BaseQuery => 
        _context.Set<TEntity>().AsExpandableEFCore().Where(e => !e.IsDeleted);

    public EntityFrameworkCoreRepository(TContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetManyAsync(
        CustomSpecification<TEntity> spec, 
        bool forReadOnly = true,
        CancellationToken ct = default)
    {
        var query = BuildQuery(spec, forReadOnly);

        return await query.ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<List<TProjection>> GetManyAsync<TProjection>(
        CustomSpecification<TEntity, TProjection> spec,
        bool forReadOnly = true, 
        CancellationToken ct = default)
    {
        var query = BuildQuery(spec, forReadOnly);

        return await query.ToListAsync(ct);
    }

    /// <inheritdoc />
    public async Task<DbOffsetPaginatedData<TEntity>> GetManyOffsetPaginatedAsync(
        CustomSpecification<TEntity> spec,
        int pageNumber = 1, 
        int pageSize = 20, 
        bool forReadOnly = true, 
        CancellationToken ct = default)
    {
        var query = BuildQuery(spec, forReadOnly);

        var data = await query.ToOffsetPaginatedListAsync(pageNumber, pageSize, ct);
        var count = await query.LongCountAsync(ct);

        var pagination = new DbOffsetPagination(count, pageNumber, pageSize);

        return new DbOffsetPaginatedData<TEntity>(data, pagination);
    }

    /// <inheritdoc />
    public async Task<DbOffsetPaginatedData<TProjection>> GetManyOffsetPaginatedAsync<TProjection>(
        CustomSpecification<TEntity, TProjection> spec, 
        int pageNumber = 1,
        int pageSize = 20, 
        bool forReadOnly = true, 
        CancellationToken ct = default)
    {
        var query = BuildQuery(spec, forReadOnly);

        var data = await query.ToOffsetPaginatedListAsync(pageNumber, pageSize, ct);
        var count = await query.LongCountAsync(ct);

        var pagination = new DbOffsetPagination(count, pageNumber, pageSize);

        return new DbOffsetPaginatedData<TProjection>(data, pagination);
    }

    /// <inheritdoc />
    public async Task<DbCursorPaginatedData<TEntity>> GetManyCursorPaginatedAsync<TCursor>(
        CustomSpecification<TEntity> spec, 
        Func<TEntity, TCursor> cursorSelector, 
        TCursor startingCursor,
        int pageSize = 20, 
        bool forReadOnly = true, 
        CancellationToken ct = default)
        where TCursor : IComparable<TCursor>
    {
        var query = BuildQuery(spec, forReadOnly);

        var cursorFilter = PredicateBuilder.New<TEntity>();
        cursorFilter.And(e => cursorSelector
            .Invoke(e)
            .CompareTo(startingCursor) >= 0);
        
        var extendedData = await query.ToCursorPaginatedListAsync(cursorFilter, pageSize, ct);
        var moreDataAvailable = extendedData.Count == pageSize + 1;

        DbCursorPagination pagination;
        if (moreDataAvailable)
        {
            var actualData = extendedData.GetRange(0, pageSize);
            var elementForNextCursor = extendedData.Last();
            var nextCursor = cursorSelector.Invoke(elementForNextCursor).ToString()!;
            
            pagination = new DbCursorPagination(nextCursor, moreDataAvailable);

            return new DbCursorPaginatedData<TEntity>(actualData, pagination);
        }

        pagination = new DbCursorPagination(string.Empty, moreDataAvailable);

        return new DbCursorPaginatedData<TEntity>(extendedData, pagination);
    }

    /// <inheritdoc />
    public async Task<DbCursorPaginatedData<TProjection>> GetManyCursorPaginatedAsync<TProjection, TCursor>(
        CustomSpecification<TEntity, TProjection> spec, 
        Func<TProjection, TCursor> cursorSelector,
        TCursor startingCursor, 
        int pageSize = 20, 
        bool forReadOnly = true, 
        CancellationToken ct = default)
        where TCursor : IComparable<TCursor>
    {
        var query = BuildQuery(spec, forReadOnly);

        var cursorFilter = PredicateBuilder.New<TProjection>();
        cursorFilter.And(e => cursorSelector.Invoke(e)
            .CompareTo(startingCursor) >= 0);
        
        var extendedData = await query.ToCursorPaginatedListAsync(cursorFilter, pageSize, ct);
        var moreDataAvailable = extendedData.Count == pageSize + 1;

        DbCursorPagination pagination;
        if (moreDataAvailable)
        {
            var actualData = extendedData.GetRange(0, pageSize);
            var elementForNextCursor = extendedData.Last();
            var nextCursor = cursorSelector.Invoke(elementForNextCursor).ToString()!;
            
            pagination = new DbCursorPagination(nextCursor, moreDataAvailable);

            return new DbCursorPaginatedData<TProjection>(actualData, pagination);
        }

        pagination = new DbCursorPagination(string.Empty, moreDataAvailable);

        return new DbCursorPaginatedData<TProjection>(extendedData, pagination);
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetOneAsync(
        CustomSpecification<TEntity> spec, 
        bool forReadOnly = true,
        CancellationToken ct = default)
    {
        var query = BuildQuery(spec, forReadOnly);

        return await query.FirstOrDefaultAsync(ct);
    }

    /// <inheritdoc />
    public async Task<TProjection?> GetOneAsync<TProjection>(
        CustomSpecification<TEntity, TProjection> spec,
        bool forReadOnly = true, 
        CancellationToken ct = default)
    {
        var query = BuildQuery(spec, forReadOnly);

        return await query.FirstOrDefaultAsync(ct);
    }

    /// <inheritdoc />
    public async Task<long> GetCountAsync(
        CustomSpecification<TEntity> spec, 
        CancellationToken ct = default)
    {
        var query = BuildQuery(spec);

        return await query.LongCountAsync(ct);
    }

    /// <inheritdoc />
    public async Task<long> GetCountAsync(CancellationToken ct = default)
    {
        return await BaseQuery.LongCountAsync(ct);
    }

    /// <inheritdoc />
    public async Task<bool> HasAnyAsync(
        CustomSpecification<TEntity> spec, 
        CancellationToken ct = default)
    {
        var query = BuildQuery(spec);

        return await query.AnyAsync(ct);
    }

    /// <inheritdoc />
    public async Task<bool> HasAnyAsync(CancellationToken ct = default)
    {
        return await BaseQuery.AnyAsync(ct);
    }

    /// <inheritdoc />
    public void AddOne(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    /// <inheritdoc />
    public void AddMany(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }

    /// <inheritdoc />
    public void UpdateOne(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    /// <inheritdoc />
    public void UpdateMany(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().UpdateRange(entities);
    }

    /// <inheritdoc />
    public void RemoveOne(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    /// <inheritdoc />
    public void RemoveMany(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    /// <summary>
    /// Генерация запроса из спецификации
    /// </summary>
    /// <param name="spec">Спецификация для построения запроса</param>
    /// <param name="forReadOnly">Включать ли в запрос AsNoTrackingWithIdentityResolution</param>
    /// <returns></returns>
    private IQueryable<TEntity> BuildQuery(
        CustomSpecification<TEntity> spec, 
        bool forReadOnly = true)
    {
        var query = BaseQuery;

        if (forReadOnly)
        {
            query = query.AsNoTrackingWithIdentityResolution();
        }

        return SpecificationEvaluator.Default.GetQuery(query, spec);
    }

    /// <summary>
    /// Генерация запроса из спецификации
    /// </summary>
    /// <param name="spec">Спецификация для построения запроса</param>
    /// <param name="forReadOnly">Включать ли в запрос AsNoTrackingWithIdentityResolution</param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    private IQueryable<TProjection> BuildQuery<TProjection>(
        CustomSpecification<TEntity, TProjection> spec,
        bool forReadOnly = true)
    {
        var query = BaseQuery;

        if (forReadOnly)
        {
            query = query.AsNoTrackingWithIdentityResolution();
        }

        return SpecificationEvaluator.Default.GetQuery(query, spec);
    }
}