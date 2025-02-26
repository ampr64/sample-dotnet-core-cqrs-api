using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Products
{
    public class ProductPriceData(ProductId productId, MoneyValue price) : ValueObject
    {
        public ProductId ProductId { get; } = productId;

        public MoneyValue Price { get; } = price;
    }
}