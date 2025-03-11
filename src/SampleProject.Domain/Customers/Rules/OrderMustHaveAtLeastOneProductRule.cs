using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules;

public class OrderMustHaveAtLeastOneProductRule(IEnumerable<OrderProductData> orderProductData) : IBusinessRule
{
    public bool IsBroken() => !orderProductData.Any();

    public string Message => "Order must have at least one product";
}