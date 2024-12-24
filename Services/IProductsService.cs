using Entities;

namespace Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProducts(int position, int skip, string? name, int? minPrice, int? maxPrice, int?[] categoriesId);
    }
}