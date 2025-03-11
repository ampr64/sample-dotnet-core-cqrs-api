using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.ForeignExchange;

public class ConversionRate(Currency sourceCurrency,
    Currency targetCurrency,
    decimal factor)
{
    public Currency SourceCurrency { get; } = sourceCurrency;

    public Currency TargetCurrency { get; } = targetCurrency;

    public decimal Factor { get; } = factor;

    internal MoneyValue Convert(MoneyValue value)
    {
        return Factor * value;
    }
}