using Shared.Infrastructure.EventSourcing.Persistence.Entities;

namespace Shared.Infrastructure.EventSourcing.Persistence.Constants;

public static class MessageStatusDefinitions
{
    public const int Undefined = 1;
    public const int ReadyToBeProduced = 2;
    public const int Produced = 3;
    public const int Consumed = 4;

    public static readonly MessageStatus[] List = 
    {
        new(Undefined, "Не задан"),
        new(ReadyToBeProduced, "Подготовлено к отправке в топик"),
        new(Produced, "Успешно отправлено в топик"),
        new(Consumed, "Успешно обработано")
    };
}