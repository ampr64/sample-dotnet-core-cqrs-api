using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Payments;

public class Payment : Entity, IAggregateRoot
{
    public PaymentId Id { get; private set; }

    private OrderId _orderId;

    private DateTime _createDate;

    private PaymentStatus _status;

    private bool _emailNotificationIsSent;

    private Payment()
    {
        // Only for EF.
    }

    public Payment(OrderId orderId)
    {
        Id = new PaymentId(Guid.NewGuid());
        _createDate = DateTime.UtcNow;
        _orderId = orderId;
        _status = PaymentStatus.ToPay;
        _emailNotificationIsSent = false;

        AddDomainEvent(new PaymentCreatedEvent(Id, _orderId));
    }

    public void MarkEmailNotificationIsSent()
    {
        _emailNotificationIsSent = true;
    }
}