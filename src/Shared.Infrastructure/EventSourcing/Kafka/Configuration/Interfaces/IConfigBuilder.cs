namespace Shared.Infrastructure.EventSourcing.Kafka.Configuration.Interfaces;

public interface IConfigBuilder<out TResult>
{
    TResult Build();
}