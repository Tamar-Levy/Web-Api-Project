using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTO;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoriesService _categoryService;
        IMapper _mapper;

        public CategoriesController(ICategoriesService categoryService, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            //_logger.LogError("\nFrom Category\n");
            IEnumerable<Category> categories = await _categoryService.GetCategories();
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
        }
    }
}
