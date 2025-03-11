using SampleProject.Application.Configuration.Commands;
using System;
using System.Collections.Generic;

namespace SampleProject.Application.Orders.PlaceCustomerOrder;

public record PlaceCustomerOrderCommand(Guid CustomerId,
    IReadOnlyList<ProductDto> Products,
    string Currency) : CommandBase<Guid>;