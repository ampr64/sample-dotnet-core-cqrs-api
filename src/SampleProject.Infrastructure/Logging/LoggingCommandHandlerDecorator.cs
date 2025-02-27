using System;
using System.Threading;
using System.Threading.Tasks;
using SampleProject.Application.Configuration;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Infrastructure.Processing.Outbox;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace SampleProject.Infrastructure.Logging;

internal class LoggingCommandHandlerDecorator<T>(
    ILogger logger,
    IExecutionContextAccessor executionContextAccessor,
    ICommandHandler<T> decorated) : ICommandHandler<T> where T : ICommand
{
    private readonly ILogger _logger = logger;

    private readonly IExecutionContextAccessor _executionContextAccessor = executionContextAccessor;

    private readonly ICommandHandler<T> _decorated = decorated;

    public async Task Handle(T command, CancellationToken cancellationToken)
    {
        if (command is IRecurringCommand)
        {
            await _decorated.Handle(command, cancellationToken);
            return;
        }

        using (
            LogContext.Push(
                new RequestLogEnricher(_executionContextAccessor),
                new CommandLogEnricher(command)))
        {
            try
            {
                this._logger.Information(
                    "Executing command {Command}",
                    command.GetType().Name);

                await _decorated.Handle(command, cancellationToken);

                this._logger.Information("Command {Command} processed successful", command.GetType().Name);

                return;
            }
            catch (Exception exception)
            {
                this._logger.Error(exception, "Command {Command} processing failed", command.GetType().Name);
                throw;
            }
        }
    }

    private class CommandLogEnricher(ICommand command) : ILogEventEnricher
    {
        private readonly ICommand _command = command;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"Command:{_command.Id.ToString()}")));
        }
    }

    private class RequestLogEnricher(IExecutionContextAccessor executionContextAccessor) : ILogEventEnricher
    {
        private readonly IExecutionContextAccessor _executionContextAccessor = executionContextAccessor;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_executionContextAccessor.IsAvailable)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId", new ScalarValue(_executionContextAccessor.CorrelationId)));
            }
        }
    }
}