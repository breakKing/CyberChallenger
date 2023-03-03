using Shared.Infrastructure.EventSourcing.Entities;

namespace Shared.Infrastructure.EventSourcing.Constants;

public static class MessageStatusesDefinition
{
    public const int Undefined = 0;
    public const int ReadyToBeProduced = 1;
    public const int Produced = 2;
    public const int Consumed = 3;

    public static MessageStatus[] List = 
    {
        new(Undefined, "Не задан"),
        new(ReadyToBeProduced, "Подготовлено к отправке в топик"),
        new(Produced, "Успешно отправлено в топик"),
        new(Consumed, "Успешно обработано")
    };
}