using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules;

public class CustomerMustHaveANameRule(string? name) : IBusinessRule
{
    public bool IsBroken() => string.IsNullOrWhiteSpace(name);

    public string Message => "Customer must have a name.";
}
