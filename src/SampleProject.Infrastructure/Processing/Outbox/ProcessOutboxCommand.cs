using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Infrastructure.Processing.Outbox;

public record ProcessOutboxCommand : CommandBase, IRecurringCommand;