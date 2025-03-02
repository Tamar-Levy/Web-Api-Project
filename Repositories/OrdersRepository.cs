using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        MyShop215736745Context _context;
        ILogger _logger;

        public OrdersRepository(MyShop215736745Context context, ILogger<OrdersRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Get
        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.Include(o=>o.User).Include(o=>o.OrderItems).FirstOrDefaultAsync(order => order.OrderId == id);
        }

        //Post
        public async Task<Order> AddOrder(Order order)
        {
            int? totalSum = await CheckOrderSum(order.OrderItems);
            if (totalSum != order.OrderSum)
                _logger.LogWarning($"Logged From Order Repository, order sum changed by {order.UserId}!");
            order.OrderSum = totalSum;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        private async Task<int?> CheckOrderSum(ICollection<OrderItem> products)
        {
            int? totalSum = 0;
            var productIds = products.Select(p => p.ProductId).ToList();

            List<Product> productsFromDB = await _context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync();
            
            foreach (var product in productsFromDB)
            {
                totalSum += product.Price;
            }

            return totalSum;
        }
    }
}
