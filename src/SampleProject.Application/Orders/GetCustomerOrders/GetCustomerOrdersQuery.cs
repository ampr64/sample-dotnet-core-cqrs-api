using SampleProject.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SampleProject.Application.Orders.GetCustomerOrders;

public record GetCustomerOrdersQuery(Guid CustomerId) : IQuery<IReadOnlyList<OrderDto>>;