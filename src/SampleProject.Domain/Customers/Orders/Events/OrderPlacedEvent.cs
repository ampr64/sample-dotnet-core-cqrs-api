using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderPlacedEvent(
        OrderId orderId,
        CustomerId customerId,
        MoneyValue value) : DomainEventBase
    {
        public OrderId OrderId { get; } = orderId;

        public CustomerId CustomerId { get; } = customerId;

        public MoneyValue Value { get; } = value;
    }
}