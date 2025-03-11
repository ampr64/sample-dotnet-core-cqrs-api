using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Customers.RegisterCustomer;

public class RegisterCustomerCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerUniquenessChecker customerUniquenessChecker,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ICustomerUniquenessChecker _customerUniquenessChecker = customerUniquenessChecker;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CustomerDto> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.CreateRegistered(request.Email, request.Name, _customerUniquenessChecker);

        await _customerRepository.AddAsync(customer);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomerDto(customer.Id.Value);
    }
}