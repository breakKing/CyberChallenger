using Ardalis.Specification;
using Shared.Infrastructure.EventSourcing.Persistence.Constants;
using Shared.Infrastructure.EventSourcing.Persistence.Entities;
using Shared.Infrastructure.Persistence.Specifications;

namespace Shared.Infrastructure.EventSourcing.Persistence.Specifications;

public sealed class GetMessagesForProduceSpec : CustomSpecification<ProducerMessage>
{
    private GetMessagesForProduceSpec()
    {
        Query.Where(o => o.Status != null && o.Status.Id == MessageStatusesDefinition.ReadyToBeProduced);

        Query.OrderBy(o => o.ProducerName);

        Query.Include(m => m.Headers);
    }

    public static GetMessagesForProduceSpec Build() => new();
}