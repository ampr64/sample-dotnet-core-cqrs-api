using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Customers.Orders;

namespace SampleProject.Application.Customers.IntegrationHandlers;

public class MarkCustomerAsWelcomedCommandHandler(ICustomerRepository customerRepository) : ICommandHandler<MarkCustomerAsWelcomedCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Unit> Handle(MarkCustomerAsWelcomedCommand command, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(command.CustomerId);

        customer.MarkAsWelcomedByEmail();

        return Unit.Value;
    }
}