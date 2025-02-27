namespace SampleProject.Domain.Products;

public interface IProductRepository
{
    Task<List<Product>> GetByIdsAsync(List<ProductId> ids);

    Task<List<Product>> GetAllAsync();
}