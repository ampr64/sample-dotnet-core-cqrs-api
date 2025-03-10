using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.SharedKernel;

public class MoneyValueMustHaveCurrencyRule(string? currency) : IBusinessRule
{
    public bool IsBroken() => string.IsNullOrEmpty(currency);

    public string Message => "Money value must have currency";
}