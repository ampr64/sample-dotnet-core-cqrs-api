using SampleProject.Domain.ForeignExchange;

namespace SampleProject.Infrastructure.Domain.ForeignExchanges;

public class ConversionRatesCache(List<ConversionRate> rates)
{
    public List<ConversionRate> Rates { get; } = rates;
}