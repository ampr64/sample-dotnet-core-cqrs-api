using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Payments;

public record PaymentCreatedEvent(PaymentId PaymentId,
    OrderId OrderId) : IDomainEvent;