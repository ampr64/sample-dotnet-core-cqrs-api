using SampleProject.Application.Orders;

namespace SampleProject.API.Orders;

public record CustomerOrderRequest(IReadOnlyList<ProductDto> Products, string Currency);