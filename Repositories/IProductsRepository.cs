using Entities;

namespace Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> Get(int? position, int? skip, string? name, int? minPrice, int? maxPrice, int?[] categoriesId);
    }
}