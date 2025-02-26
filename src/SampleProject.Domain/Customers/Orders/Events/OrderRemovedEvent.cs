using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderRemovedEvent(OrderId orderId) : DomainEventBase
    {
        public OrderId OrderId { get; } = orderId;
    }
}