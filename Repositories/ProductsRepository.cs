using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        MyShop215736745Context _context;

        public ProductsRepository(MyShop215736745Context context)
        {
            _context = context;
        }

        //Get
        public async Task<IEnumerable<Product>> Get(int? position, int? skip, string? name, int? minPrice, int? maxPrice, int?[] categoriesId)
        {
            var query = _context.Products.Include(p => p.Category).Where(product =>
            (name == null ? (true) : (product.ProductName.Contains(name)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoriesId.Length == 0) ? (true) : (categoriesId.Contains(product.CategoryId))))
                .OrderBy(product => product.Price);
            IEnumerable<Product> products = await query.ToListAsync();
            return products;
        }

        public async Task<Product> AddProduct(Product product)
        {

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
