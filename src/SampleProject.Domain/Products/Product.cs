using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Products;

public class Product : Entity, IAggregateRoot
{
    public ProductId Id { get; private init; }

    public string Name { get; private set; } = null!;

    private readonly List<ProductPrice> _prices = [];

    private Product()
    {
    }
}