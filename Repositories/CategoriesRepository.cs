using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        MyShop215736745Context _context;//

        public CategoriesRepository(MyShop215736745Context context)
        {
            _context = context;
        }

        //Get
        public async Task<IEnumerable<Category>> Get()
        {
            return await _context.Categories.Include(c=>c.Products).ToListAsync();
        }

        // GetById
        public async Task<Category> GetById(int id)
        {
            Category categoryFound = await _context.Categories.FirstOrDefaultAsync(category => category.CategoryId == id);
            return categoryFound;
        }
    }
}
