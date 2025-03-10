using NUnit.Framework;
using SampleProject.Domain.SharedKernel;
using SampleProject.UnitTests.SeedWork;

namespace SampleProject.UnitTests.SharedKernel;

[TestFixture]
public class MoneyValueTests : TestBase
{
    [TestCaseSource(nameof(CurrencyCases))]
    public void MoneyValueOf_WhenCurrencyIsProvided_IsSuccessful(Currency currency)
    {
        var value = MoneyValue.Of(120, currency);

        Assert.That(value.Value, Is.EqualTo(120));
        Assert.That(value.Currency, Is.EqualTo(currency));
    }

    [TestCase(null)]
    [TestCase("")]
    public void MoneyValueOf_WhenCurrencyIsNotProvided_ThrowsMoneyValueMustHaveCurrencyRuleBroken(string? currency)
    {
        AssertBrokenRule<MoneyValueMustHaveCurrencyRule>(() =>
        {
            MoneyValue.Of(120, currency!);
        });
    }
    
    [TestCase("PLN")]
    [TestCase("ARS")]
    public void MoneyValueOf_WhenCurrencyIsNotSupported_ThrowsCurrencyMustBeSupportedRuleBroken(string currency)
    {
        AssertBrokenRule<CurrencyMustBeSupportedRule>(() =>
        {
            MoneyValue.Of(120, currency);
        });
    }

    [TestCaseSource(nameof(CurrencyCases))]
    public void GivenTwoMoneyValuesWithTheSameCurrencies_WhenAddThem_IsSuccessful(Currency currency)
    {
        var valueA = MoneyValue.Of(100, currency);
        var valueB = MoneyValue.Of(50, currency);

        var sum = valueA + valueB;

        Assert.That(sum.Value, Is.EqualTo(150));
        Assert.That(sum.Currency, Is.EqualTo(currency));
    }

    [TestCaseSource(nameof(CurrencyCases))]
    public void GivenTwoMoneyValuesWithTheSameCurrencies_SumThem_IsSuccessful(Currency currency)
    {
        var valueInEuros = MoneyValue.Of(100, currency);
        var valueInEuros2 = MoneyValue.Of(50, currency);

        List<MoneyValue> values = [valueInEuros, valueInEuros2];

        var sum = values.Sum();

        Assert.That(sum.Value, Is.EqualTo(150));
        Assert.That(sum.Currency, Is.EqualTo(currency));
    }

    [Test]
    public void GivenTwoMoneyValuesWithDifferentCurrencies_WhenAddThem_ThrowsMoneyValueOperationMustBePerformedOnTheSameCurrencyRule()
    {
        var valueInEuros = MoneyValue.Of(100, "EUR");
        var valueInDollars = MoneyValue.Of(50, "USD");
        AssertBrokenRule<MoneyValueOperationMustBePerformedOnTheSameCurrencyRule>(() =>
        {
            var add = valueInEuros + valueInDollars;
        });
    }

    private static readonly Currency[] CurrencyCases =
        [Currency.UsDollar, Currency.Euro];
}