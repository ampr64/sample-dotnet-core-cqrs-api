using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders;

public class OrderProduct : Entity
{
    public int Quantity { get; private set; }

    public ProductId ProductId { get; private set; }

    internal MoneyValue Value { get; private set; } = null!;

    internal MoneyValue ValueInEUR { get; private set; } = null!;

    private OrderProduct()
    {
    }

    private OrderProduct(
        ProductPriceData productPrice,
        int quantity,
        Currency currency,
        IEnumerable<ConversionRate> conversionRates)
    {
        ProductId = productPrice.ProductId;
        Quantity = quantity;

        CalculateValue(productPrice, currency, conversionRates);
    }

    internal static OrderProduct CreateForProduct(ProductPriceData productPrice,
        int quantity,
        Currency currency,
        IEnumerable<ConversionRate> conversionRates)
    {
        return new OrderProduct(productPrice, quantity, currency, conversionRates);
    }

    internal void ChangeQuantity(ProductPriceData productPrice, int quantity, IReadOnlyList<ConversionRate> conversionRates)
    {
        Quantity = quantity;

        CalculateValue(productPrice, Value.Currency, conversionRates);
    }

    private void CalculateValue(ProductPriceData productPrice, Currency currency, IEnumerable<ConversionRate> conversionRates)
    {
        Value = Quantity * productPrice.Price;
        if (currency == Currency.Euro)
        {
            ValueInEUR = Quantity * productPrice.Price;
        }
        else
        {
            var conversionRate = conversionRates.Single(x => x.SourceCurrency == currency && x.TargetCurrency == Currency.Euro);
            ValueInEUR = conversionRate.Convert(Value);
        }
    }
}