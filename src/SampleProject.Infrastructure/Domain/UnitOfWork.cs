using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Database;
using SampleProject.Infrastructure.Processing;

namespace SampleProject.Infrastructure.Domain;

public class UnitOfWork(
    OrdersContext ordersContext,
    IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
{
    private readonly OrdersContext _ordersContext = ordersContext;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher = domainEventsDispatcher;

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await _domainEventsDispatcher.DispatchEventsAsync();
        return await _ordersContext.SaveChangesAsync(cancellationToken);
    }
}