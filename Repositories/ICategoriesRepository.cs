using Entities;

namespace Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> Get();
        Task<Category> GetById(int id);
    }
}