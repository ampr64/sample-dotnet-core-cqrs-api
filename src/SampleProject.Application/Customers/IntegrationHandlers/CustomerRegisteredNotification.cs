using SampleProject.Application.Configuration.DomainEvents;
using SampleProject.Domain.Customers;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Customers.IntegrationHandlers;

public class CustomerRegisteredNotification : DomainNotificationBase<CustomerRegisteredEvent>
{
    public CustomerId CustomerId { get; }

    public CustomerRegisteredNotification(CustomerRegisteredEvent domainEvent) : base(domainEvent)
    {
        CustomerId = domainEvent.CustomerId;
    }

    [JsonConstructor]
    public CustomerRegisteredNotification(CustomerId customerId) : base(null)
    {
        CustomerId = customerId;
    }
}