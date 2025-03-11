using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Orders.PlaceCustomerOrder;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Orders.ChangeCustomerOrder;

internal sealed class ChangeCustomerOrderCommandHandler : ICommandHandler<ChangeCustomerOrderCommand>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IForeignExchange _foreignExchange;

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    internal ChangeCustomerOrderCommandHandler(
        ICustomerRepository customerRepository,
        IForeignExchange foreignExchange,
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _customerRepository = customerRepository;
        _foreignExchange = foreignExchange;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task Handle(ChangeCustomerOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

        var orderId = new OrderId(request.OrderId);

        var conversionRates = _foreignExchange.GetConversionRates();
        var orderProducts = request
                .Products
                .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
                .ToList();

        var allProductPrices =
            await ProductPriceProvider.GetAllProductPrices(_sqlConnectionFactory.GetOpenConnection());

        customer.ChangeOrder(
            orderId,
            allProductPrices,
            orderProducts,
            conversionRates,
            request.Currency);
    }
}
