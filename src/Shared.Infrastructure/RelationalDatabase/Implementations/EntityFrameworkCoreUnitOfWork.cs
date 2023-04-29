using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Infrastructure.RelationalDatabase.Entities;
using Shared.Infrastructure.RelationalDatabase.Interfaces;

namespace Shared.Infrastructure.RelationalDatabase.Implementations;

public sealed class EntityFrameworkCoreUnitOfWork<TContext> : IGenericUnitOfWork
    where TContext : DbContext
{
    private readonly TContext _context;
    
    private IDbContextTransaction? DbTransaction { get; set; }

    public EntityFrameworkCoreUnitOfWork(TContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
    {
        return new EntityFrameworkCoreRepository<TEntity, TContext>(_context);
    }

    /// <inheritdoc />
    public async Task StartTransactionAsync(CancellationToken ct = default)
    {
        if (DbTransaction is not null)
        {
            throw new InvalidOperationException("Transaction has already been started");
        }
        
        DbTransaction = await _context.Database.BeginTransactionAsync(ct);
    }

    /// <inheritdoc />
    public async Task CommitAsync(CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(DbTransaction);
        await _context.SaveChangesAsync(ct);
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