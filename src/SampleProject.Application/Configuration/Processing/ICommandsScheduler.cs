using System.Threading.Tasks;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Configuration.Processing;

public interface ICommandsScheduler
{
    Task EnqueueAsync(ICommand command);

    Task EnqueueAsync<T>(ICommand<T> command);
}