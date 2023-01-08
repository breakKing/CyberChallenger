using System.Linq.Expressions;
using Ardalis.Specification;
using LinqKit;

namespace Shared.Infrastructure.Persistence.Extensions;

public static class SpecificationBuilderExtensions
{
    /// <summary>
    /// Добавление в спецификации поиска по списку составных объектов (с точки зрения SQL - LIKE с массивом составных объектов).
    /// </summary>
    /// <param name="query">Билдер спецификации (см. <see cref="Specification{T}.Query"/>)</param>
    /// <param name="entitiesToSearch">Массив с элементами, с которыми надо сравнивать сущность из БД</param>
    /// <param name="comparatorLambda">Компаратор для сравнения</param>
    /// <typeparam name="TEntity">Сущность из БД</typeparam>
    /// <typeparam name="TDto">Составной объект, с которым производится сравнение сущности</typeparam>
    /// <returns>Дополненный объект билдера спецификации</returns>
    public static ISpecificationBuilder<TEntity> WhereCompositeContains<TEntity, TDto>(
        this ISpecificationBuilder<TEntity> query,
        List<TDto> entitiesToSearch, Expression<Func<TEntity, TDto, bool>> comparatorLambda)
    {
        if (entitiesToSearch.Count == 0)
        {
            return query;
        }
        
        var finalLambda = PredicateBuilder.New<TEntity>();
        foreach (var entityDto in entitiesToSearch)
        {
            finalLambda.Or(e => comparatorLambda.Invoke(e, entityDto));
        }

        query.Where(finalLambda);

        return query;
    }

    /// <summary>
    /// Добавление в спецификации поиска по списку составных объектов (с точки зрения SQL - LIKE с массивом составных объектов).
    /// Перегрузка для спецификации, которая возвращает проекцию сущности из БД, а не её саму.
    /// </summary>
    /// <param name="query">Билдер спецификации (см. <see cref="Specification{T,TResult}.Query"/>)</param>
    /// <param name="entitiesToSearch">Массив с элементами, с которыми надо сравнивать сущность из БД</param>
    /// <param name="comparatorLambda">Компаратор для сравнения</param>
    /// <typeparam name="TEntity">Сущность из БД</typeparam>
    /// <typeparam name="TDto">Составной объект, с которым производится сравнение сущности</typeparam>
    /// <typeparam name="TProjection">Проекция сущности БД, выгружаемая из БД</typeparam>
    /// <returns>Дополненный объект билдера спецификации</returns>
    public static ISpecificationBuilder<TEntity> WhereCompositeContains<TEntity, TDto, TProjection>(
        this ISpecificationBuilder<TEntity, TProjection> query,
        List<TDto> entitiesToSearch, Expression<Func<TEntity, TDto, bool>> comparatorLambda)
    {
        if (entitiesToSearch.Count == 0)
        {
            return query;
        }
        
        var finalLambda = PredicateBuilder.New<TEntity>();
        foreach (var entityDto in entitiesToSearch)
        {
            finalLambda.Or(e => comparatorLambda.Invoke(e, entityDto));
        }

        query.Where(finalLambda);

        return query;
    }
}