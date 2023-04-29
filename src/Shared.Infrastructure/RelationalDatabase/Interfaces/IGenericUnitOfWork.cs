using Shared.Infrastructure.RelationalDatabase.Entities;

namespace Shared.Infrastructure.RelationalDatabase.Interfaces;

/// <summary>
/// Работа с БД через репозитории
/// </summary>
public interface IGenericUnitOfWork : IDisposable
{
    /// <summary>
    /// Акцессор для репозиториев
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;

    /// <summary>
    /// Открытие транзакции
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task StartTransactionAsync(CancellationToken ct = default);

    /// <summary>
    /// Коммит открытой транзакции
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task CommitAsync(CancellationToken ct = default);

    /// <summary>
    /// Откат открытой транзакции
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task RollbackAsync(CancellationToken ct = default);
}