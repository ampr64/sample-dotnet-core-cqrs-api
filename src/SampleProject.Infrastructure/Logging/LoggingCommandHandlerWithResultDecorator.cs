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

internal class LoggingCommandHandlerWithResultDecorator<T, TResult>(
    ILogger logger,
    IExecutionContextAccessor executionContextAccessor,
    ICommandHandler<T, TResult> decorated) : ICommandHandler<T, TResult> where T : ICommand<TResult>
{
    private readonly ILogger _logger = logger;
    private readonly IExecutionContextAccessor _executionContextAccessor = executionContextAccessor;
    private readonly ICommandHandler<T, TResult> _decorated = decorated;

    public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
    {
        if (command is IRecurringCommand)
        {
            return await _decorated.Handle(command, cancellationToken);
        }

        using (
            LogContext.Push(
                new RequestLogEnricher(_executionContextAccessor),
                new CommandLogEnricher(command)))
        {
            try
            {
                this._logger.Information(
                    "Executing command {@Command}",
                    command);

                var result = await _decorated.Handle(command, cancellationToken);

                this._logger.Information("Command processed successful, result {Result}", result);

                return result;
            }
            catch (Exception exception)
            {
                this._logger.Error(exception, "Command processing failed");
                throw;
            }
        }
    }

    private class CommandLogEnricher(ICommand<TResult> command) : ILogEventEnricher
    {
        private readonly ICommand<TResult> _command = command;

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