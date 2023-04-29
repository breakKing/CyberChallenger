using Ardalis.Specification;

namespace Shared.Infrastructure.RelationalDatabase.Specifications;

public abstract class CustomSpecification<TEntity> : Specification<TEntity, TEntity>
{
    
}

public abstract class CustomSpecification<TEntity, TProjection> : Specification<TEntity, TProjection>
{
    
}