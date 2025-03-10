using System;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Orders.GetCustomerOrderDetails;

public record GetCustomerOrderDetailsQuery(Guid OrderId) : IQuery<OrderDetailsDto>;