using System;

namespace SampleProject.Application.Orders;

public record ProductDto(Guid Id, int Quantity)
{
}