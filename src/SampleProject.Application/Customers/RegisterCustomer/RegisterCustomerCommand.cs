using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Customers.RegisterCustomer;

public class RegisterCustomerCommand(string email, string name) : CommandBase<CustomerDto>
{
    public string Email { get; } = email;

    public string Name { get; } = name;
}