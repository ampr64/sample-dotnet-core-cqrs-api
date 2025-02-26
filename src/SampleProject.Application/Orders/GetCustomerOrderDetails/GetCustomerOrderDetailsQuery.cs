using System;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Orders.GetCustomerOrderDetails
{
    public class GetCustomerOrderDetailsQuery(Guid orderId) : IQuery<OrderDetailsDto>
    {
        public Guid OrderId { get; } = orderId;
    }
}