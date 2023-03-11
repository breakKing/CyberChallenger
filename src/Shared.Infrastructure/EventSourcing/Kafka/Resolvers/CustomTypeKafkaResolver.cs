using System.Reflection;
using KafkaFlow;

namespace Shared.Infrastructure.EventSourcing.Kafka.Resolvers;

[Obsolete("Отказ от резолвера в пользу полноценной интеграции SchemaRegistry")]
public sealed class CustomTypeKafkaResolver : IMessageTypeResolver
{
    private const string MessageType = "message_type";
    
    /// <inheritdoc />
    public Type OnConsume(IMessageContext context)
    {
        var typeName = context.Headers.GetString(MessageType) + ", " + Assembly.GetExecutingAssembly().GetName().Name;

        return Type.GetType(typeName)!;
    }

    /// <inheritdoc />
    public void OnProduce(IMessageContext context)
    {
        context.Headers.SetString(MessageType, context.Message.Value.GetType().FullName);
    }
}