using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules;

public class CustomerMustHaveAnEmailRule(string? email) : IBusinessRule
{
    public bool IsBroken() => string.IsNullOrWhiteSpace(email);

    public string Message => "Customer must have an email.";
}