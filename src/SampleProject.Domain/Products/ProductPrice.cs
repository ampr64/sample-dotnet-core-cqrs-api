using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Products;

public class ProductPrice
{
    public required MoneyValue Value { get; init; }

    private ProductPrice()
    {
    }
}