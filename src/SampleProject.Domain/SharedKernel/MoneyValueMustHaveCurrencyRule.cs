using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.SharedKernel;

public class MoneyValueMustHaveCurrencyRule(string currency) : IBusinessRule
{
    private readonly string _currency = currency;

    public bool IsBroken() => string.IsNullOrEmpty(_currency);

    public string Message => "Money value must have currency";
}