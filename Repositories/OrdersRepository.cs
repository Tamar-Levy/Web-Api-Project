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

        public OrdersRepository(MyShop215736745Context context)
        {
            _context = context;
        }

        //Get
        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.Include(o=>o.User).Include(o=>o.OrderItems).FirstOrDefaultAsync(order => order.OrderId == id);
        }

        //Post
        public async Task<Order> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

     
    }
}
