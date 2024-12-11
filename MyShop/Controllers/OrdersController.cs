using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<Order> Get(int id)
        {
            return await _ordersService.GetById(id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<Order> Post([FromBody] Order order)
        {
            return await _ordersService.AddOrder(order);
        }
    }
}
