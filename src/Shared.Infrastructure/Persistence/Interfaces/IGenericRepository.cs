using Shared.Infrastructure.Persistence.Entities;
using Shared.Infrastructure.Persistence.Models.Dto;
using Shared.Infrastructure.Persistence.Specifications;

namespace Shared.Infrastructure.Persistence.Interfaces;

public interface IGenericRepository<TEntity>
    where TEntity : EntityBase
{
    /// <summary>
    /// Получение списка сущностей, удовлетворяющих спецификации
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="forReadOnly"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetManyAsync(CustomSpecification<TEntity> spec, bool forReadOnly = true,
        CancellationToken ct = default);

    /// <summary>
    /// Получение списка сущностей, удовлетворяющих спецификации
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="forReadOnly"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<List<TProjection>> GetManyAsync<TProjection>(CustomSpecification<TEntity, TProjection> spec,
        bool forReadOnly = true,
        CancellationToken ct = default);

    /// <summary>
    /// Получение списка сущностей, удовлетворяющих спецификации, с учётом пагинации
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="forReadOnly"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<DbPaginatedData<TEntity>> GetManyPaginatedAsync(CustomSpecification<TEntity> spec, int pageNumber = 1,
        int pageSize = 20, bool forReadOnly = true, CancellationToken ct = default);

    /// <summary>
    /// Получение списка сущностей, удовлетворяющих спецификации, с учётом пагинации
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="forReadOnly"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<DbPaginatedData<TProjection>> GetManyPaginatedAsync<TProjection>(CustomSpecification<TEntity, TProjection> spec,
        int pageNumber = 1,
        int pageSize = 20, bool forReadOnly = true, CancellationToken ct = default);

    /// <summary>
    /// Получение одной сущности, удовлетворяющей спецификации (или ни одной, если её нет)
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="forReadOnly"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TEntity?> GetOneAsync(CustomSpecification<TEntity> spec, bool forReadOnly = true,
        CancellationToken ct = default);

    /// <summary>
    /// Получение одной сущности, удовлетворяющей спецификации (или ни одной, если её нет)
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="forReadOnly"></param>
    /// <param name="ct"></param>
    /// <typeparam name="TProjection"></typeparam>
    /// <returns></returns>
    Task<TProjection?> GetOneAsync<TProjection>(CustomSpecification<TEntity, TProjection> spec,
        bool forReadOnly = true, CancellationToken ct = default);

    /// <summary>
    /// Получение количества сущностей, удовлетворяющих спецификации
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<long> GetCountAsync(CustomSpecification<TEntity> spec, CancellationToken ct = default);

    /// <summary>
    /// Получение количества всех сущностей
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<long> GetCountAsync(CancellationToken ct = default);

    /// <summary>
    /// Проверка существования хотя бы одной сущности, удовлетворяющей спецификации
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> HasAnyAsync(CustomSpecification<TEntity> spec, CancellationToken ct = default);

    /// <summary>
    /// Проверка существования хотя бы одной сущности
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> HasAnyAsync(CancellationToken ct = default);

    /// <summary>
    /// Добавление новой сущности
    /// </summary>
    /// <param name="entity"></param>
    void AddOne(TEntity entity);

    /// <summary>
    /// Добавление нескольких новых сущностей
    /// </summary>
    /// <param name="entities"></param>
    void AddMany(IEnumerable<TEntity> entities);

    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="entity"></param>
    void UpdateOne(TEntity entity);

    /// <summary>
    /// Обновление нескольких сущностей
    /// </summary>
    /// <param name="entities"></param>
    void UpdateMany(IEnumerable<TEntity> entities);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="entity"></param>
    void RemoveOne(TEntity entity);

    /// <summary>
    /// Удаление нескольких сущностей
    /// </summary>
    /// <param name="entities"></param>
    void RemoveMany(IEnumerable<TEntity> entities);
}