using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Infrastructure.Persistence.Entities;
using Shared.Infrastructure.Persistence.Interfaces;

namespace Shared.Infrastructure.Persistence.Implementations;

public sealed class GenericUnitOfWork<TContext> : IGenericUnitOfWork<TContext>
    where TContext : DbContext
{
    private readonly TContext _context;
    
    private IDbContextTransaction? DbTransaction { get; set; }

    public GenericUnitOfWork(TContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
    {
        return new GenericRepository<TEntity, TContext>(_context);
    }

    /// <inheritdoc />
    public async Task StartTransactionAsync(CancellationToken ct = default)
    {
        DbTransaction ??= await _context.Database.BeginTransactionAsync(ct);
    }

    /// <inheritdoc />
    public async Task CommitAsync(CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(DbTransaction);
        await DbTransaction.CommitAsync(ct);
        DbTransaction = null;
    }

    /// <inheritdoc />
    public async Task RollbackAsync(CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(DbTransaction);
        await DbTransaction.RollbackAsync(ct);
        DbTransaction = null;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _context.Dispose();
    }
}