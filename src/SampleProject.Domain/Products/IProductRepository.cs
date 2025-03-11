namespace SampleProject.Domain.Products;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<ProductId> ids);

    Task<IReadOnlyList<Product>> GetAllAsync();
}