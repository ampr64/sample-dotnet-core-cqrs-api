namespace SampleProject.Domain.ForeignExchange;

public interface IForeignExchange
{
    IReadOnlyList<ConversionRate> GetConversionRates();
}