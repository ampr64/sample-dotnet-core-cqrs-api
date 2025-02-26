using System;
using System.Collections.Generic;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommand(
        Guid customerId,
        List<ProductDto> products,
        string currency) : CommandBase<Guid>
    {
        public Guid CustomerId { get; } = customerId;

        public List<ProductDto> Products { get; } = products;

        public string Currency { get; } = currency;
    }
}