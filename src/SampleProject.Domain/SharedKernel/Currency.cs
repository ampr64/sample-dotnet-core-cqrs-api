using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.SharedKernel;

public class Currency : ValueObject
{
    public string Code { get; }

    private Currency(string name) => Code = name;

    public static Currency Of(string name)
    {
        var currency = new Currency(name);
        CheckRule(new CurrencyMustBeSupportedRule(currency));

        return currency;
    }

    public override string ToString() => Code;

    public static Currency UsDollar => new("USD");

    public static Currency Euro => new("EUR");

    internal static IEnumerable<Currency> SupportedCurrencies
    {
        get
        {
            yield return UsDollar;
            yield return Euro;
        }
    }

    public static implicit operator string(Currency currency) => currency.Code;

}