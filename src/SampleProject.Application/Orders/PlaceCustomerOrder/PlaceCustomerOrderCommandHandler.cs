using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Orders.PlaceCustomerOrder;

public class PlaceCustomerOrderCommandHandler(
    ICustomerRepository customerRepository,
    IForeignExchange foreignExchange,
    ISqlConnectionFactory sqlConnectionFactory) : ICommandHandler<PlaceCustomerOrderCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    private readonly IForeignExchange _foreignExchange = foreignExchange;

    public async Task<Guid> Handle(PlaceCustomerOrderCommand command, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));

        var allProductPrices =
            await ProductPriceProvider.GetAllProductPrices(_sqlConnectionFactory.GetOpenConnection());

        var conversionRates = _foreignExchange.GetConversionRates();

        var orderProductsData = command
            .Products
            .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
            .ToList();

        var orderId = customer.PlaceOrder(
            orderProductsData,
            allProductPrices,
            command.Currency,
            conversionRates);

        return orderId.Value;
    }
}