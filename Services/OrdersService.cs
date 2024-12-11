using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrdersService : IOrdersService
    {
        IOrdersRepository _ordersRepository;
        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Order> GetById(int id)
        {
            return await _ordersRepository.GetById(id);
        }

        public async Task<Order> AddOrder(Order order)
        {
            return await _ordersRepository.AddOrder(order);
        }
    }
}
