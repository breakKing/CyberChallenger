using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EventSourcing.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Persistence.Configurations;

public sealed class ProducerMessageConfig : IEntityTypeConfiguration<ProducerMessage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProducerMessage> builder)
    {
        builder.ToTable("outbox", "event_sourcing");
        
        builder.HasIndex(o => o.ProducerName);

        builder.HasOne(o => o.Status)
            .WithMany()
            .HasForeignKey(o => o.StatusId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}