using System.Globalization;
using Ardalis.Specification;
using Shared.Infrastructure.EventSourcing.Persistence.Constants;
using Shared.Infrastructure.EventSourcing.Persistence.Entities;
using Shared.Infrastructure.Persistence.Specifications;

namespace Shared.Infrastructure.EventSourcing.Persistence.Specifications;

public sealed class GetConsumedMessagesSpec : CustomSpecification<ConsumerMessage>
{
    private GetConsumedMessagesSpec(string consumerName, string topicName, string messageType, string messageKey, 
        byte[] messageValue, DateTimeOffset producedAt)
    {
        Query.Where(m => m.ConsumerName == consumerName
                         && m.TopicName == topicName
                         && m.Type == messageType
                         && m.Key == messageKey
                         && m.Value == messageValue
                         && m.Status != null
                         && m.Status.Id == MessageStatusDefinitions.Consumed
                         && m.Headers != null
                         && m.Headers.Any(h => h.Key == HeaderDefinitions.ProducedAt
                                               && h.Value == producedAt.ToString(CultureInfo.InvariantCulture)));

        Query.OrderByDescending(m => m.UpdatedAt);

        Query.Include(m => m.Headers);

        Query.Select(m => m);
    }

    public static GetConsumedMessagesSpec Build(string consumerName, string topicName, string messageType, string messageKey,
        byte[] messageValue, DateTimeOffset producedAt) => new (consumerName, topicName, messageType, messageKey, messageValue, producedAt);
}