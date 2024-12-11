using Entities;

namespace Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}