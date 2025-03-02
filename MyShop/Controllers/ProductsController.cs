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
        ILogger<ProductsController> _logger;

        public ProductsController(IProductsService productsService, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> Get([FromQuery] int position, [FromQuery] int skip, [FromQuery] string? name, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoriesId)
        {
            _logger.LogInformation("The application load succsessfully!!");
            IEnumerable<Product> products = await _productsService.GetProducts(position,skip, name, minPrice,maxPrice,categoriesId);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }
    }
}
