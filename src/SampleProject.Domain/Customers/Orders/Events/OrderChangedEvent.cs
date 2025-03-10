using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events;

public record OrderChangedEvent(OrderId OrderId) : IDomainEvent;