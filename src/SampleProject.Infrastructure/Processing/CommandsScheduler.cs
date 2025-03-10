using Dapper;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Configuration.Processing;
using System.Text.Json;

namespace SampleProject.Infrastructure.Processing;

public class CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory) : ICommandsScheduler
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task EnqueueAsync(ICommand command)
        => await EnqueueAsyncCore(command);

    public async Task EnqueueAsync<T>(ICommand<T> command)
        => await EnqueueAsyncCore(command);

    private async Task EnqueueAsyncCore(object command)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sqlInsert = """
            INSERT INTO [app].[InternalCommands]
            ([Id], [EnqueueDate], [Type], [Data]) VALUES
            (@Id, @EnqueueDate, @Type, @Data)
            """;

        var commandType = command.GetType();
        var id = commandType.GetProperty(nameof(ICommand.Id))?.GetValue(command);

        await connection.ExecuteAsync(sqlInsert, new
        {
            Id = id,
            EnqueueDate = DateTime.UtcNow,
            Type = commandType.FullName,
            Data = JsonSerializer.Serialize(command)
        });
    }
}