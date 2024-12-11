using Entities;

namespace Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> Get();
    }
}