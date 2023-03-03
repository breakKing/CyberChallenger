using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EventSourcing.Constants;
using Shared.Infrastructure.EventSourcing.Entities;

namespace Shared.Infrastructure.EventSourcing.Configurations;

public sealed class MessageStatusConfig : IEntityTypeConfiguration<MessageStatus>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<MessageStatus> builder)
    {
        builder.ToTable("message_statuses", "outbox");

        builder.HasData(MessageStatusesDefinition.List);
    }
}