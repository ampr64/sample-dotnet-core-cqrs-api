namespace SampleProject.Infrastructure.Processing;

public interface IDomainEventsDispatcher
{
    Task DispatchEventsAsync();
}