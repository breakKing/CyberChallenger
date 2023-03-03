using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EventSourcing.Entities;

namespace Shared.Infrastructure.EventSourcing.Configurations;

public sealed class ConsumerMessageConfig : IEntityTypeConfiguration<ConsumerMessage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ConsumerMessage> builder)
    {
        builder.ToTable("consumed_messages", "outbox");

        builder.HasIndex(o => o.ConsumerName);

        builder.HasOne(o => o.Status)
            .WithMany()
            .HasForeignKey(o => o.StatusId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}