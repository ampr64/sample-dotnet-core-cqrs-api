using NUnit.Framework;
using SampleProject.Domain.SharedKernel;
using SampleProject.UnitTests.SeedWork;

namespace SampleProject.UnitTests.SharedKernel;

[TestFixture]
public class CurrencyTests : TestBase
{
    [TestCaseSource(nameof(CurrencyCodes))]
    public void CurrencyOf_WhenCodeIsValid_CreatesCurrency(string code)
    {
        var currency = Currency.Of(code);

        Assert.That(currency, Is.Not.Null);
        Assert.That(currency.Code, Is.EqualTo(code));
    }

    [TestCase("GBP")]
    [TestCase("ARS")]
    public void CurrencyOf_WhenCodeIsNotSupported_ThrowsCurrencyMustBeSupportedRuleBroken(string currency)
    {
        AssertBrokenRule<CurrencyMustBeSupportedRule>(() =>
        {
            MoneyValue.Of(120, currency);
        });
    }

    private static readonly string[] CurrencyCodes =
        [Currency.UsDollar.Code, Currency.Euro.Code];
}