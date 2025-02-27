using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Payments;

public class PaymentCreatedEvent(PaymentId paymentId, OrderId orderId) : DomainEventBase
{
    public PaymentId PaymentId { get; } = paymentId;

    public OrderId OrderId { get; } = orderId;
}