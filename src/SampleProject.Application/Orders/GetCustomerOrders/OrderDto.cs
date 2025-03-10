using System;

namespace SampleProject.Application.Orders.GetCustomerOrders;

public record OrderDto(Guid Id, decimal Value, string Currency, bool IsRemoved);
