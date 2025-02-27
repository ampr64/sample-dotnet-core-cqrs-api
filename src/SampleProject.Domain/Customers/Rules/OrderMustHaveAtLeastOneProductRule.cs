using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules;

public class OrderMustHaveAtLeastOneProductRule(List<OrderProductData> orderProductData) : IBusinessRule
{
    private readonly List<OrderProductData> _orderProductData = orderProductData;

    public bool IsBroken() => !_orderProductData.Any();

    public string Message => "Order must have at least one product";
}