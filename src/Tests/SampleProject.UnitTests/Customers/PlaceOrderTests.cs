using NUnit.Framework;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers.Orders.Events;
using SampleProject.Domain.Customers.Rules;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using SampleProject.Domain.SharedKernel;
using SampleProject.UnitTests.SeedWork;

namespace SampleProject.UnitTests.Customers;

[TestFixture]
public class PlaceOrderTests : TestBase
{
    [Test]
    public void PlaceOrder_WhenAtLeastOneProductIsAdded_IsSuccessful()
    {
        // Arrange
        var customer = CustomerFactory.Create();

        var orderProductsData = new List<OrderProductData>
        {
            new(SampleProducts.Product1Id, 2)
        };

        var allProductPrices = new List<ProductPriceData>
        {
            SampleProductPrices.Product1EUR, SampleProductPrices.Product1USD
        };

        var conversionRates = GetConversionRates();

        // Act
        customer.PlaceOrder(
            orderProductsData,
            allProductPrices,
            Currency.Euro,
            conversionRates);

        // Assert
        var orderPlaced = AssertPublishedDomainEvent<OrderPlacedEvent>(customer);
        Assert.That(orderPlaced.Value, Is.EqualTo(MoneyValue.Of(200, Currency.Euro)));
    }

    [Test]
    public void PlaceOrder_WhenNoProductIsAdded_BreaksOrderMustHaveAtLeastOneProductRule()
    {
        // Arrange
        var customer = CustomerFactory.Create();

        var orderProductsData = new List<OrderProductData>();

        var allProductPrices = new List<ProductPriceData>
        {
            SampleProductPrices.Product1EUR, SampleProductPrices.Product1USD
        };

        const string currency = "EUR";
        var conversionRates = GetConversionRates();

        // Assert
        AssertBrokenRule<OrderMustHaveAtLeastOneProductRule>(() =>
        {
            // Act
            customer.PlaceOrder(
                orderProductsData,
                allProductPrices,
                currency,
                conversionRates);
        });
    }

    [Test]
    public void PlaceOrder_GivenTwoOrdersInThatDayAlreadyMade_BreaksCustomerCannotOrderMoreThanTwiceADayRule()
    {
        // Arrange
        var customer = CustomerFactory.Create();

        var orderProductsData = new List<OrderProductData>
        {
            new(SampleProducts.Product1Id, 2)
        };

        var allProductPrices = new List<ProductPriceData>
        {
            SampleProductPrices.Product1EUR, SampleProductPrices.Product1USD
        };

        const string currency = "EUR";
        var conversionRates = GetConversionRates();

        SystemClock.Set(new DateTime(2020, 1, 10, 11, 0, 0));
        customer.PlaceOrder(
            orderProductsData,
            allProductPrices,
            currency,
            conversionRates);

        SystemClock.Set(new DateTime(2020, 1, 10, 11, 30, 0));
        customer.PlaceOrder(
            orderProductsData,
            allProductPrices,
            currency,
            conversionRates);

        SystemClock.Set(new DateTime(2020, 1, 10, 12, 00, 0));

        // Assert
        AssertBrokenRule<CustomerCannotOrderMoreThanTwiceADayRule>(() =>
        {
            // Act
            customer.PlaceOrder(
                orderProductsData,
                allProductPrices,
                currency,
                conversionRates);
        });
    }

    private static List<ConversionRate> GetConversionRates()
    {
        List<ConversionRate> conversionRates =
        [
            new(Currency.UsDollar, Currency.Euro, (decimal)0.88),
            new(Currency.Euro, Currency.UsDollar, (decimal)1.13)
        ];

        return conversionRates;
    }
}

public class SampleProducts
{
    public static readonly ProductId Product1Id = new(Guid.NewGuid());

    public static readonly ProductId Product2Id = new(Guid.NewGuid());
}

public class SampleProductPrices
{
    public static readonly ProductPriceData Product1EUR = new(
        SampleProducts.Product1Id,
        MoneyValue.Of(100, Currency.Euro));

    public static readonly ProductPriceData Product1USD = new(
        SampleProducts.Product1Id,
        MoneyValue.Of(110, Currency.UsDollar));
}