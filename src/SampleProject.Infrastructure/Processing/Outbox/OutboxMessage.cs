namespace SampleProject.Infrastructure.Processing.Outbox;

public record OutboxMessage(DateTime OccurredOn, string Type, string Data)
{
    public Guid Id { get; } = Guid.NewGuid();
}