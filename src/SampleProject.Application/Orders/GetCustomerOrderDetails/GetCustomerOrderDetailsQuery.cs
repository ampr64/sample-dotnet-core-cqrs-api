using SampleProject.Application.Configuration.Queries;
using System;

namespace SampleProject.Application.Orders.GetCustomerOrderDetails;

public record GetCustomerOrderDetailsQuery(Guid OrderId) : IQuery<OrderDetailsDto>;