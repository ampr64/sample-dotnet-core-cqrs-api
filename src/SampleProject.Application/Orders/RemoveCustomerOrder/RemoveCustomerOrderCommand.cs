using SampleProject.Application.Configuration.Commands;
using System;

namespace SampleProject.Application.Orders.RemoveCustomerOrder;

public record RemoveCustomerOrderCommand(Guid CustomerId,
    Guid OrderId) : CommandBase;