using Entities;

namespace Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}