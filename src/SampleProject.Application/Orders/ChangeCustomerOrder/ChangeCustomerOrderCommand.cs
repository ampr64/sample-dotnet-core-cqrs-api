using System;
using System.Collections.Generic;
using MediatR;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Orders.ChangeCustomerOrder;

public class ChangeCustomerOrderCommand(
    Guid customerId,
    Guid orderId,
    List<ProductDto> products,
    string currency) : CommandBase<Unit>
{
    public Guid CustomerId { get; } = customerId;

    public Guid OrderId { get; } = orderId;

    public string Currency { get; } = currency;

    public List<ProductDto> Products { get; } = products;
}
