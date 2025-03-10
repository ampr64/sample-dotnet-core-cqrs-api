using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Customers.RegisterCustomer;

public record RegisterCustomerCommand(string Email, string Name) : CommandBase<CustomerDto>;