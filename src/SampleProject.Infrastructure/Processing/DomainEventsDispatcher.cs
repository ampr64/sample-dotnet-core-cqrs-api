using Autofac;
using Autofac.Core;
using MediatR;
using SampleProject.Application.Configuration.DomainEvents;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Database;
using SampleProject.Infrastructure.Processing.Outbox;
using System.Text.Json;

namespace SampleProject.Infrastructure.Processing;

public class DomainEventsDispatcher(IMediator mediator, ILifetimeScope scope, OrdersContext ordersContext) : IDomainEventsDispatcher
{
    private readonly IMediator _mediator = mediator;
    private readonly ILifetimeScope _scope = scope;
    private readonly OrdersContext _ordersContext = ordersContext;

    public async Task DispatchEventsAsync()
    {
        var domainEntities = this._ordersContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
        foreach (var domainEvent in domainEvents)
        {
            Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
            var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
            var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
            {
                new NamedParameter("domainEvent", domainEvent)
            });

            if (domainNotification != null)
            {
                domainEventNotifications.Add((domainNotification as IDomainEventNotification<IDomainEvent>)!);
            }
        }

        domainEntities
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await _mediator.Publish(domainEvent);
            });

        await Task.WhenAll(tasks);

        foreach (var domainEventNotification in domainEventNotifications)
        {
            var type = domainEventNotification.GetType().FullName!;
            var data = JsonSerializer.Serialize(domainEventNotification);
            OutboxMessage outboxMessage = new OutboxMessage(
                domainEventNotification.DomainEvent.OccurredOn,
                type,
                data);
            this._ordersContext.OutboxMessages.Add(outboxMessage);
        }
    }
}