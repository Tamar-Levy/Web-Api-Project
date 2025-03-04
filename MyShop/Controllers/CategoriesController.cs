using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTO;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoriesService _categoryService;
        IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public CategoriesController(ICategoriesService categoryService, IMapper mapper, ILogger<CategoriesController> logger, IMemoryCache memoryCache)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            //    //_logger.LogError("\nFrom Category\n");
            //    IEnumerable<Category> categories = await _categoryService.GetCategories();
            //    return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
            //}
            if (!_memoryCache.TryGetValue("CategoriesCache", out IEnumerable<Category> categories))
            {
                categories = await _categoryService.GetCategories();

                if (categories == null || !categories.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };

                _memoryCache.Set("CategoriesCache", categories, cacheEntryOptions);
            }

            return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories));
        }
    }
}
