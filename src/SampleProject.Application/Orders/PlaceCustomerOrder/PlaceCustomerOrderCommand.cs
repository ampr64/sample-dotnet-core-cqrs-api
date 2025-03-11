using System;
using System.Collections.Generic;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Orders.PlaceCustomerOrder;

public record PlaceCustomerOrderCommand(Guid CustomerId,
    IReadOnlyList<ProductDto> Products,
    string Currency) : CommandBase<Guid>;