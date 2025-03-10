using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules;

public class CustomerEmailMustBeUniqueRule(ICustomerUniquenessChecker customerUniquenessChecker,
    string email) : IBusinessRule
{
    public bool IsBroken() => !customerUniquenessChecker.IsUnique(email);

    public string Message => "Customer with this email already exists.";
}