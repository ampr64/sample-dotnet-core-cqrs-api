using System.Threading;
using System.Threading.Tasks;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Database;
using SampleProject.Infrastructure.Processing;

namespace SampleProject.Infrastructure.Domain
{
    public class UnitOfWork(
        OrdersContext ordersContext,
        IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
    {
        private readonly OrdersContext _ordersContext = ordersContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher = domainEventsDispatcher;

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this._domainEventsDispatcher.DispatchEventsAsync();
            return await this._ordersContext.SaveChangesAsync(cancellationToken);
        }
    }
}