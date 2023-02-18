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

    /// <summary>
    /// Добавить фильтр, если значение для фильтрации задано <br/>
    /// Подходит для числовых фильтров, фильтров-гуидов и дат
    /// </summary>
    /// <param name="query">Билдер спецификации (см. <see cref="Specification{T,TResult}.Query"/>)</param>
    /// <param name="filter">Значение для фильтрации</param>
    /// <param name="expression">Условие для фильтрации</param>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TFilter">Тип фильтра</typeparam>
    /// <returns></returns>
    public static ISpecificationBuilder<TEntity> FilterIfPresented<TEntity, TFilter>(
        this ISpecificationBuilder<TEntity> query, TFilter? filter, Expression<Func<TEntity, bool>> expression)
        where TFilter : struct
    {
        return filter.HasValue ? query.Where(expression) : query;
    }
    
    /// <summary>
    /// Добавить фильтр, если строковое значение для фильтрации задано <br/>
    /// </summary>
    /// <param name="query">Билдер спецификации (см. <see cref="Specification{T,TResult}.Query"/>)</param>
    /// <param name="filter">Строка для фильтрации</param>
    /// <param name="expression">Условие для фильтрации</param>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <returns></returns>
    public static ISpecificationBuilder<TEntity> FilterIfPresented<TEntity>(
        this ISpecificationBuilder<TEntity> query, string? filter, Expression<Func<TEntity, bool>> expression)
    {
        return !string.IsNullOrWhiteSpace(filter) ? query.Where(expression) : query;
    }
    
    /// <summary>
    /// Добавить фильтр, если список значений для фильтрации задан
    /// </summary>
    /// <param name="query">Билдер спецификации (см. <see cref="Specification{T,TResult}.Query"/>)</param>
    /// <param name="filter">Список значений для фильтрации</param>
    /// <param name="expression">Условие для фильтрации</param>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TValue">Тип элемента списка</typeparam>
    /// <returns></returns>
    public static ISpecificationBuilder<TEntity> FilterIfPresented<TEntity, TValue>(
        this ISpecificationBuilder<TEntity> query, List<TValue>? filter, Expression<Func<TEntity, bool>> expression)
    {
        return filter is { Count: > 0 } ? query.Where(expression) : query;
    }
    
    /// <summary>
    /// Добавить фильтр, если список значений для фильтрации задан
    /// </summary>
    /// <param name="query">Билдер спецификации (см. <see cref="Specification{T,TResult}.Query"/>)</param>
    /// <param name="filter">Список значений для фильтрации</param>
    /// <param name="expression">Условие для фильтрации</param>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TValue">Тип элемента списка</typeparam>
    /// <returns></returns>
    public static ISpecificationBuilder<TEntity> FilterIfPresented<TEntity, TValue>(
        this ISpecificationBuilder<TEntity> query, IEnumerable<TValue>? filter, 
        Expression<Func<TEntity, bool>> expression)
    {
        return filter is not null && filter.Any() ? query.Where(expression) : query;
    }
}