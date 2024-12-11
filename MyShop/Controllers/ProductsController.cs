using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductsService _productsService;
        IMapper _mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            //return await _productsService.GetProducts();
            IEnumerable<Product> products = await _productsService.GetProducts();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }
    }
}
