using MediatR;

namespace SampleProject.Domain.SeedWork;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn => DateTime.UtcNow;
}