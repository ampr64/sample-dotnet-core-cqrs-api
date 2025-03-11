using SampleProject.Application.Configuration.Commands;
using System.Threading.Tasks;

namespace SampleProject.Application.Configuration.Processing;

public interface ICommandsScheduler
{
    Task EnqueueAsync(ICommand command);

    Task EnqueueAsync<T>(ICommand<T> command);
}