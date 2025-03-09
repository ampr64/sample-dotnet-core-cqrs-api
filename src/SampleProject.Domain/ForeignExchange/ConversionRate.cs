using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.ForeignExchange;

public class ConversionRate(string sourceCurrency, string targetCurrency, decimal factor)
{
    public string SourceCurrency { get; } = sourceCurrency;

    public string TargetCurrency { get; } = targetCurrency;

    public decimal Factor { get; } = factor;

    internal MoneyValue Convert(MoneyValue value)
    {
        return Factor * value;
    }
}