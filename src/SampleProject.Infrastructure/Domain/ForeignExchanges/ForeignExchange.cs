using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.SharedKernel;
using SampleProject.Infrastructure.Caching;

namespace SampleProject.Infrastructure.Domain.ForeignExchanges;

public class ForeignExchange(ICacheStore cacheStore) : IForeignExchange
{
    private readonly ICacheStore _cacheStore = cacheStore;

    public IReadOnlyList<ConversionRate> GetConversionRates()
    {
        var ratesCache = _cacheStore.Get(new ConversionRatesCacheKey());

        if (ratesCache != null)
        {
            return ratesCache.Rates;
        }

        var rates = GetConversionRatesFromExternalApi();

        _cacheStore.Add(new ConversionRatesCache(rates), new ConversionRatesCacheKey(), DateTime.UtcNow.Date.AddDays(1));

        return rates;
    }

    private static List<ConversionRate> GetConversionRatesFromExternalApi()
    {
        // Communication with external API. Here is only mock.

        List<ConversionRate> conversionRates =
        [
            new(Currency.UsDollar, Currency.Euro, (decimal)0.88),
            new(Currency.Euro, Currency.UsDollar, (decimal)1.13)
        ];

        return conversionRates;
    }
}