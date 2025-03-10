using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Infrastructure.Processing.Outbox;

namespace SampleProject.Infrastructure.Processing.InternalCommands;

internal record ProcessInternalCommandsCommand : CommandBase, IRecurringCommand;