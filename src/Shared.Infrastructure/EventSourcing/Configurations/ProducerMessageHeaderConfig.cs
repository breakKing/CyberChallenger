﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EventSourcing.Entities;

namespace Shared.Infrastructure.EventSourcing.Configurations;

public sealed class ProducerMessageHeaderConfig : IEntityTypeConfiguration<ProducerMessageHeader>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProducerMessageHeader> builder)
    {
        builder.ToTable("message_headers", "outbox");

        builder.HasKey(h => new { h.MessageId, h.Key });

        builder.HasOne(h => h.Message)
            .WithMany(m => m.Headers)
            .HasForeignKey(h => h.MessageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(h => h.Key)
            .IsRequired();
    }
}