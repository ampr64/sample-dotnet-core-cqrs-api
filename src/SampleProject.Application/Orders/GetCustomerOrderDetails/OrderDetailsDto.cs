using System;
using System.Collections.Generic;

namespace SampleProject.Application.Orders.GetCustomerOrderDetails;

public record OrderDetailsDto(Guid Id, decimal Value, string Currency, bool IsRemoved, List<ProductDto> Products);
