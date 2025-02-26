using SampleProject.Domain.Products;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderProductData(ProductId productId, int quantity)
    {
        public ProductId ProductId { get; } = productId;

        public int Quantity { get; } = quantity;
    }
}