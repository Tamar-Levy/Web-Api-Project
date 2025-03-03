using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        IProductsRepository _productsRepository;
        ILogger<OrdersRepository> _logger;

        public OrdersService(IOrdersRepository ordersRepository, ILogger<OrdersRepository> logger, IProductsRepository productsRepository)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;
            _productsRepository = productsRepository;
        }

        public async Task<Order> GetById(int id)
        {
            return await _ordersRepository.GetById(id);
        }

        public async Task<Order> AddOrder(Order order)
        {
            int? totalSum = await CheckOrderSum(order.OrderItems);
            if (totalSum != order.OrderSum)
                _logger.LogWarning($"Logged From Order Repository, order sum changed by {order.UserId}!");
            order.OrderSum = totalSum;
            return await _ordersRepository.AddOrder(order);
        }

        private async Task<int?> CheckOrderSum(ICollection<OrderItem> products)
        {
            int? totalSum = 0;
            var productIds = products.Select(p => p.ProductId).ToList();

            int?[] a = new int?[0];
            IEnumerable<Product> productsFromDB = await _productsRepository.Get(null,null, null, null, null, a) ;

            foreach (var product in productsFromDB)
            {
                if(productIds.Contains(product.ProductId))
                    totalSum += product.Price;
            }

            return totalSum;
        }
    }
}
