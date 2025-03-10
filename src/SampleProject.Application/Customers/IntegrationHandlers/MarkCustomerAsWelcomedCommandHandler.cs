using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Customers.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Customers.IntegrationHandlers;

public class MarkCustomerAsWelcomedCommandHandler(ICustomerRepository customerRepository) : ICommandHandler<MarkCustomerAsWelcomedCommand>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task Handle(MarkCustomerAsWelcomedCommand command, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(command.CustomerId);

        customer.MarkAsWelcomedByEmail();
    }
}