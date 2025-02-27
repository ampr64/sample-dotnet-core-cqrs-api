using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules;

public class CustomerEmailMustBeUniqueRule(
    ICustomerUniquenessChecker customerUniquenessChecker,
    string email) : IBusinessRule
{
    private readonly ICustomerUniquenessChecker _customerUniquenessChecker = customerUniquenessChecker;

    private readonly string _email = email;

    public bool IsBroken() => !_customerUniquenessChecker.IsUnique(_email);

    public string Message => "Customer with this email already exists.";
}