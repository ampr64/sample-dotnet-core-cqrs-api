using SampleProject.Application.Orders;

namespace SampleProject.API.Orders;

public record CustomerOrderRequest(List<ProductDto> Products, string Currency);