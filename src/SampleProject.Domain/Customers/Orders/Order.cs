using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders;

public class Order : Entity
{
    internal OrderId Id;

    private bool _isRemoved;

    private MoneyValue _value = null!;

    private MoneyValue _valueInEUR = null!;

    private readonly List<OrderProduct> _orderProducts = [];

    private OrderStatus _status;

    private DateTime _orderDate;

    private DateTime? _orderChangeDate;

    private Order()
    {
    }

    private Order(IReadOnlyList<OrderProductData> orderProductsData,
        IReadOnlyList<ProductPriceData> productPrices,
        Currency currency,
        IReadOnlyList<ConversionRate> conversionRates)
    {
        _orderDate = SystemClock.Now;
        Id = new OrderId(Guid.NewGuid());

        foreach (var orderProductData in orderProductsData)
        {
            var productPrice = productPrices.Single(x => x.ProductId == orderProductData.ProductId &&
                                                         x.Price.Currency == currency);
            var orderProduct = OrderProduct.CreateForProduct(
                productPrice,
                orderProductData.Quantity,
                currency,
                conversionRates);

            _orderProducts.Add(orderProduct);
        }

        CalculateOrderValue();
        _status = OrderStatus.Placed;
    }

    internal static Order CreateNew(IReadOnlyList<OrderProductData> orderProductsData,
        IReadOnlyList<ProductPriceData> allProductPrices,
        Currency currency,
        IReadOnlyList<ConversionRate> conversionRates)
    {
        return new Order(orderProductsData, allProductPrices, currency, conversionRates);
    }

    internal void Change(
        IReadOnlyList<ProductPriceData> allProductPrices,
        IReadOnlyList<OrderProductData> orderProductsData,
        IReadOnlyList<ConversionRate> conversionRates,
        Currency currency)
    {
        foreach (var orderProductData in orderProductsData)
        {
            var product = allProductPrices.Single(x => x.ProductId == orderProductData.ProductId &&
                                                       x.Price.Currency == currency);

            var existingProductOrder = _orderProducts.SingleOrDefault(x => x.ProductId == orderProductData.ProductId);
            if (existingProductOrder != null)
            {
                var existingOrderProduct = _orderProducts.Single(x => x.ProductId == existingProductOrder.ProductId);

                existingOrderProduct.ChangeQuantity(product, orderProductData.Quantity, conversionRates);
            }
            else
            {
                var orderProduct = OrderProduct.CreateForProduct(product, orderProductData.Quantity, currency, conversionRates);
                _orderProducts.Add(orderProduct);
            }
        }

        var orderProductsToCheck = _orderProducts.ToList();
        foreach (var existingProduct in orderProductsToCheck)
        {
            var product = orderProductsData.SingleOrDefault(x => x.ProductId == existingProduct.ProductId);
            if (product == null)
            {
                _orderProducts.Remove(existingProduct);
            }
        }

        CalculateOrderValue();

        _orderChangeDate = DateTime.UtcNow;
    }

    internal void Remove()
    {
        _isRemoved = true;
    }

    internal bool IsOrderedToday()
    {
        return _orderDate.Date == SystemClock.Now.Date;
    }

    internal MoneyValue GetValue()
    {
        return _value;
    }

    private void CalculateOrderValue()
    {
        _value = _orderProducts.Sum(x => x.Value);

        _valueInEUR = _orderProducts.Sum(x => x.ValueInEUR);
    }
}