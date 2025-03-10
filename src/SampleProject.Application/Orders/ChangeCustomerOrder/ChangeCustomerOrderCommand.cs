using SampleProject.Application.Configuration.Commands;
using System;
using System.Collections.Generic;

namespace SampleProject.Application.Orders.ChangeCustomerOrder;

public record ChangeCustomerOrderCommand(Guid CustomerId,
    Guid OrderId,
    List<ProductDto> Products,
    string Currency) : CommandBase;
