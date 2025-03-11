using System;
using System.Collections.Generic;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Orders.GetCustomerOrders;

public record GetCustomerOrdersQuery(Guid CustomerId) : IQuery<IReadOnlyList<OrderDto>>;