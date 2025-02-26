using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Processing
{
    public class UnitOfWorkCommandHandlerDecorator<T>(
        ICommandHandler<T> decorated,
        IUnitOfWork unitOfWork,
        OrdersContext ordersContext) : ICommandHandler<T> where T:ICommand
    {
        private readonly ICommandHandler<T> _decorated = decorated;

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly OrdersContext _ordersContext = ordersContext;

        public async Task Handle(T command, CancellationToken cancellationToken)
        {
            await this._decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                var internalCommand =
                    await _ordersContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id,
                        cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await this._unitOfWork.CommitAsync(cancellationToken);
        }
    }
}