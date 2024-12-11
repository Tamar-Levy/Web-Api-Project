using Entities;

namespace Services
{
    public interface IOrdersService
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetById(int id);
    }
}