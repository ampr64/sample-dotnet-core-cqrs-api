namespace SampleProject.Infrastructure.Processing.Outbox;

public record OutboxMessageDto(Guid Id, string Type, string Data);