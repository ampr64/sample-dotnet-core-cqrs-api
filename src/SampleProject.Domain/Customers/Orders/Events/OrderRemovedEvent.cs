using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events;

public record OrderRemovedEvent(OrderId OrderId) : IDomainEvent;