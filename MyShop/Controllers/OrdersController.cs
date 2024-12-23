using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrdersService _ordersService;
        IMapper _mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<OrderDTO> Get(int id)
        {
            Order order = await _ordersService.GetById(id);
            return _mapper.Map<Order, OrderDTO>(order);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<Order> Post([FromBody] PostOrderDTO orderDTO)
        {
            Order order = _mapper.Map<PostOrderDTO, Order>(orderDTO);
            return await _ordersService.AddOrder(order);
        }
    }
}
