using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsProject
{
    public class ProductRepositoryUnitTest
    {
        [Fact]
        public async Task Get_ReturnsFilteredProductsByPrice()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Product1", Price = 100, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Product2", Price = 200, CategoryId = 2 },
                new Product { ProductId = 3, ProductName = "Product3", Price = 300, CategoryId = 1 }
            };

            var mockContext = new Mock<MyShop215736745Context>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            var productRepository = new ProductsRepository(mockContext.Object);

            // Act
            var result = await productRepository.Get(null, null, null, 100, 300, new int?[] { });

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Contains(result, p => p.ProductId == 1);
            Assert.Contains(result, p => p.ProductId == 2);
            Assert.Contains(result, p => p.ProductId == 3);
        }

        [Fact]
        public async Task Get_ReturnsFilteredProductsByName()
        { 
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Product1", Price = 100, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Product2", Price = 200, CategoryId = 2 },
                new Product { ProductId = 3, ProductName = "Product3", Price = 300, CategoryId = 1 }
            };

            var mockContext = new Mock<MyShop215736745Context>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            var productRepository = new ProductsRepository(mockContext.Object);

            // Act
            var result = await productRepository.Get(null, null, "Product2", null, null, new int?[] { });

            // Assert
            Assert.Single(result);
            Assert.Equal("Product2", result.First().ProductName);
        }
    }
}



