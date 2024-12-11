using Entities;

namespace Repositories
{
    public interface IOrdersRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetById(int id);
    }
}