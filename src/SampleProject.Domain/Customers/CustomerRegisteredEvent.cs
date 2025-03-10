using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers;

public record CustomerRegisteredEvent(CustomerId CustomerId) : IDomainEvent;