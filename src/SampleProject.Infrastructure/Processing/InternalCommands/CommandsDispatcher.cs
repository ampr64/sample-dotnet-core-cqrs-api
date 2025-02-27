using System;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SampleProject.Application.Customers;
using SampleProject.Infrastructure.Database;

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
        dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

        internalCommand.ProcessedDate = DateTime.UtcNow;

        await this._mediator.Send(command);
    }
}