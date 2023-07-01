namespace Common.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    /// <inheritdoc />
    protected AggregateRoot(Guid id) : base(id)
    {
        
    }
}