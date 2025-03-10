using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders.Events;

public record OrderPlacedEvent(OrderId OrderId,
    CustomerId CustomerId,
    MoneyValue Value) : IDomainEvent;