using Dapper;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Configuration.Processing;
using System.Text.Json;

namespace SampleProject.Infrastructure.Processing;

public class CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory) : ICommandsScheduler
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task EnqueueAsync<T>(ICommand<T> command)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sqlInsert = """
            INSERT INTO [app].[InternalCommands]
            ([Id], [EnqueueDate], [Type], [Data]) VALUES
            (@Id, @EnqueueDate, @Type, @Data)
            """;

        await connection.ExecuteAsync(sqlInsert, new
        {
            command.Id,
            EnqueueDate = DateTime.UtcNow,
            Type = command.GetType().FullName,
            Data = JsonSerializer.Serialize(command)
        });
    }
}