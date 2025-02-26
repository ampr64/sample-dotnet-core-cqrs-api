using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers
{
    public class CustomerRegisteredEvent(CustomerId customerId) : DomainEventBase
    {
        public CustomerId CustomerId { get; } = customerId;
    }
}