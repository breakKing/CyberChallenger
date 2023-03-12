using System.Text;
using KafkaFlow;

namespace Shared.Infrastructure.EventSourcing.Kafka.Extensions;

public static class MessageContextExtensions
{
    public static string GetMessageKey(this IMessageContext context)
    {
        if (context.Message.Key is byte[] rawKey)
        {
            return Encoding.Default.GetString(rawKey);
        }
        
        return string.Empty;
    }
    
    public static string GetMessageValue(this IMessageContext context)
    {
        if (context.Message.Value is byte[] rawValue)
        {
            return Encoding.Default.GetString(rawValue);
        }
        
        return string.Empty;
    }
}