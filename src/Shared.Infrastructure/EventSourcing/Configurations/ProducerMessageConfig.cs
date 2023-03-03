﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EventSourcing.Entities;

namespace Shared.Infrastructure.EventSourcing.Configurations;

public sealed class ProducerMessageConfig : IEntityTypeConfiguration<ProducerMessage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProducerMessage> builder)
    {
        builder.ToTable("produced_messages", "outbox");
        
        builder.HasIndex(o => o.ProducerName);

        builder.HasOne(o => o.Status)
            .WithMany()
            .HasForeignKey(o => o.StatusId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}