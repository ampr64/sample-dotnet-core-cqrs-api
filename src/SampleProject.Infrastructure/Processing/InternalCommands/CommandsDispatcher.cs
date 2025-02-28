using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Customers;
using SampleProject.Infrastructure.Database;
using System.Reflection;
using System.Text.Json;

namespace SampleProject.Infrastructure.Processing.InternalCommands;

public class CommandsDispatcher(
    IMediator mediator,
    OrdersContext ordersContext) : ICommandsDispatcher
{
    private readonly IMediator _mediator = mediator;
    private readonly OrdersContext _ordersContext = ordersContext;

    public async Task DispatchCommandAsync(Guid id)
    {
        var internalCommand = await this._ordersContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

        Type type = Assembly.GetAssembly(typeof(MarkCustomerAsWelcomedCommand)).GetType(internalCommand.Type);
        dynamic command = JsonSerializer.Deserialize(internalCommand.Data, type);

        internalCommand.ProcessedDate = DateTime.UtcNow;

        await this._mediator.Send(command);
    }
}