using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.SharedKernel;

public class CurrencyMustBeSupportedRule(Currency currency) : IBusinessRule
{
    public bool IsBroken() => !Currency.SupportedCurrencies.Contains(currency);

    public string Message => $"Currency '{currency}' is not supported";
}