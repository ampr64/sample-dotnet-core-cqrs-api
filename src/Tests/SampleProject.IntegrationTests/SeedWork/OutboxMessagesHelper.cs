using Dapper;
using MediatR;
using SampleProject.Application.Payments;
using SampleProject.Infrastructure.Processing.Outbox;
using System.Data;
using System.Reflection;
using System.Text.Json;

namespace SampleProject.IntegrationTests.SeedWork;

public class OutboxMessagesHelper
{
    public static async Task<List<OutboxMessageDto>> GetOutboxMessages(IDbConnection connection)
    {
        const string sql = """
                           SELECT
                           [OutboxMessage].[Id],
                           [OutboxMessage].[Type],
                           [OutboxMessage].[Data]
                           FROM [app].[OutboxMessages] AS [OutboxMessage]
                           ORDER BY [OutboxMessage].[OccurredOn]
                           """;

        var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
        return messages.AsList();
    }

    public static T Deserialize<T>(OutboxMessageDto message) where T : class, INotification
    {
        var type = Assembly.GetAssembly(typeof(PaymentCreatedNotification))!.GetType(message.Type)!;
        return (JsonSerializer.Deserialize(message.Data, type) as T)!;
    }
}