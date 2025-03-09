using SampleProject.Application.Configuration.DomainEvents;
using SampleProject.Domain.Payments;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Payments;

public class PaymentCreatedNotification : DomainNotificationBase<PaymentCreatedEvent>
{
    public PaymentId PaymentId { get; }

    public PaymentCreatedNotification(PaymentCreatedEvent domainEvent) : base(domainEvent)
    {
        PaymentId = domainEvent.PaymentId;
    }

    [JsonConstructor]
    public PaymentCreatedNotification(PaymentId paymentId) : base(null)
    {
        PaymentId = paymentId;
    }
}