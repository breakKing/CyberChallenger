using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EventSourcing.Persistence.Constants;
using Shared.Infrastructure.EventSourcing.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Persistence.Configurations;

public sealed class MessageStatusConfig : IEntityTypeConfiguration<MessageStatus>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<MessageStatus> builder)
    {
        builder.ToTable("message_statuses", "event_sourcing");

        builder.HasData(MessageStatusesDefinition.List);
    }
}