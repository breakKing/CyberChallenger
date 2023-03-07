using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EventSourcing.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Persistence.Configurations;

public sealed class ConsumerMessageConfig : IEntityTypeConfiguration<ConsumerMessage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ConsumerMessage> builder)
    {
        builder.ToTable("inbox", "event_sourcing");

        builder.HasIndex(i => i.ConsumerName);

        builder.HasOne(i => i.Status)
            .WithMany()
            .HasForeignKey(i => i.StatusId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}