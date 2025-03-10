using System;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Orders.RemoveCustomerOrder;

public record RemoveCustomerOrderCommand(Guid CustomerId,
    Guid OrderId) : CommandBase;