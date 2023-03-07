using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Shared.Infrastructure.EventSourcing.Persistence.Configurations;

public sealed class EventSourcingModelCustomizer : RelationalModelCustomizer
{
    /// <inheritdoc />
    public EventSourcingModelCustomizer(ModelCustomizerDependencies dependencies) : base(dependencies)
    {
    }
    
    /// <inheritdoc />
    public override void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        if (modelBuilder is null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        modelBuilder.ApplyConfiguration(new ConsumerMessageConfig());
        modelBuilder.ApplyConfiguration(new ConsumerMessageHeaderConfig());
        modelBuilder.ApplyConfiguration(new MessageStatusConfig());
        modelBuilder.ApplyConfiguration(new ProducerMessageConfig());
        modelBuilder.ApplyConfiguration(new ProducerMessageHeaderConfig());
        
        base.Customize(modelBuilder, context);
    }
}