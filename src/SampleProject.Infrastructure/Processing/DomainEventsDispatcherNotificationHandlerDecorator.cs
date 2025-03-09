using MediatR;

namespace SampleProject.Infrastructure.Processing;

public class DomainEventsDispatcherNotificationHandlerDecorator<T>(
    IDomainEventsDispatcher domainEventsDispatcher,
    INotificationHandler<T> decorated) : INotificationHandler<T> where T : INotification
{
    private readonly INotificationHandler<T> _decorated = decorated;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher = domainEventsDispatcher;

    public async Task Handle(T notification, CancellationToken cancellationToken)
    {
        await _decorated.Handle(notification, cancellationToken);

        await _domainEventsDispatcher.DispatchEventsAsync();
    }
}
