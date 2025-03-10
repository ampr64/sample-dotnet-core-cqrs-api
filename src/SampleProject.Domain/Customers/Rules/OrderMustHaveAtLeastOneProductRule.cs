using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules;

public class OrderMustHaveAtLeastOneProductRule(List<OrderProductData> orderProductData) : IBusinessRule
{
    public bool IsBroken() => orderProductData.Count.Equals(0);

    public string Message => "Order must have at least one product";
}