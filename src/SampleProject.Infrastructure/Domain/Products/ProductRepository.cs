using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Products;
using SampleProject.Infrastructure.Database;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Domain.Products;

public class ProductRepository(OrdersContext context) : IProductRepository
{
    private readonly OrdersContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<ProductId> ids)
    {
        return await _context
            .Products
            .IncludePaths("_prices")
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        return await _context
            .Products
            .IncludePaths("_prices")
            .ToListAsync();
    }
}