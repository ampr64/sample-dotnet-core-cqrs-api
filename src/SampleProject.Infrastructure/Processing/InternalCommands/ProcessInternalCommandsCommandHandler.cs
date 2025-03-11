using Dapper;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;
using System.Text.Json;

namespace SampleProject.Infrastructure.Processing.InternalCommands;

internal class ProcessInternalCommandsCommandHandler(
    ISqlConnectionFactory sqlConnectionFactory) : ICommandHandler<ProcessInternalCommandsCommand>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = """
                           SELECT
                           [Command].[Type],
                           [Command].[Data]
                           FROM [app].[InternalCommands] AS [Command]
                           WHERE [Command].[ProcessedDate] IS NULL
                           """;

        var commands = await connection.QueryAsync<InternalCommandDto>(sql);

        var internalCommandsList = commands.AsList();

        foreach (var internalCommand in internalCommandsList)
        {
            var type = Assemblies.Application.GetType(internalCommand.Type)!;
            dynamic? commandToProcess = JsonSerializer.Deserialize(internalCommand.Data, type);

            await CommandsExecutor.Execute(commandToProcess);
        }
    }

    private record InternalCommandDto(string Type, string Data);
}