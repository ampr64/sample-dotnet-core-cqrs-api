using SampleProject.Domain.ForeignExchange;

namespace SampleProject.Infrastructure.Domain.ForeignExchanges;

public class ConversionRatesCache(IReadOnlyList<ConversionRate> rates)
{
    public IReadOnlyList<ConversionRate> Rates { get; } = rates;
}