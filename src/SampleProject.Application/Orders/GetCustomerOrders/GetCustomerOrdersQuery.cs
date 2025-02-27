using System;
using System.Collections.Generic;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Orders.GetCustomerOrders;

public class GetCustomerOrdersQuery(Guid customerId) : IQuery<List<OrderDto>>
{
    public Guid CustomerId { get; } = customerId;
}