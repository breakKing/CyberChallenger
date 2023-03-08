using System.Text;
using KafkaFlow;

namespace Shared.Infrastructure.EventSourcing.Kafka.Extensions;

public static class MessageContextExtensions
{
    public static string GetMessageKey(this IMessageContext context)
    {
        return Encoding.Unicode.GetString((byte[])context.Message.Key);
    }
    
    public static string GetMessageValue(this IMessageContext context)
    {
        return Encoding.Unicode.GetString((byte[])context.Message.Value);
    }
}