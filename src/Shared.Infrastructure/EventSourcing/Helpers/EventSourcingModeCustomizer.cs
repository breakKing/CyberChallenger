using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Infrastructure.EventSourcing.Configurations;

namespace Shared.Infrastructure.EventSourcing.Helpers;

public sealed class EventSourcingModeCustomizer : RelationalModelCustomizer
{
    /// <inheritdoc />
    public EventSourcingModeCustomizer(ModelCustomizerDependencies dependencies) : base(dependencies)
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