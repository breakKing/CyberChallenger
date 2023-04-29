namespace Shared.Infrastructure.Messaging.Configuration;

public sealed class MessagingConfiguration
{
    public const string SectionName = "Messaging";

    public string KafkaAddress { get; set; } = string.Empty;
    
    public int ConsumerThreadCount { get; set; } = 1;
    public int ProducerThreadCount { get; set; } = 1;
    
    public string ConsumerGroupName { get; set; } = string.Empty;
    
    public int SucceedMessageExpiredAfterSeconds { get; set; } = (int)TimeSpan.FromDays(1).TotalSeconds;
    public int FailedMessageExpiredAfterSeconds { get; set; } = (int)TimeSpan.FromDays(30).TotalSeconds;
    
    public int FailedRetryCount { get; set; } = 100;
    public int FailedRetryIntervalSeconds { get; set; } = (int)TimeSpan.FromMinutes(2).TotalSeconds;

    public int CleanupIntervalSeconds { get; set; } = (int)TimeSpan.FromMinutes(30).TotalSeconds;
}