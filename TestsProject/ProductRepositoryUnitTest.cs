using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsProject
{
    internal class ProductRepositoryUnitTest
    {

            private readonly Mock<MyShop215736745Context> _mockContext;
            private readonly ProductsRepository _repository;

            public ProductRepositoryUnitTest()
            {
                _mockContext = new Mock<MyShop215736745Context>();
                _repository = new ProductsRepository(_mockContext.Object);
            }

            [Fact]
            public async Task Get_ReturnsFilteredProducts()
            {
                // Arrange
                var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Phone", Price = 1000, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Laptop", Price = 2500, CategoryId = 2 },
                new Product { ProductId = 3, ProductName = "Shirt", Price = 100, CategoryId = 3 }
            }.AsQueryable();

                var dbSetMock = new Mock<DbSet<Product>>();
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

                _mockContext.Setup(c => c.Products).Returns(dbSetMock.Object);

                // Act
                var result = await _repository.Get(null, null, "Phone", null, null, new int[] { });

                // Assert
                Assert.Single(result);
                Assert.Equal("Phone", result.First().ProductName);
            }

            [Fact]
            public async Task Get_ReturnsProductsWithinPriceRange()
            {
                // Arrange
                var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Phone", Price = 1000, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Laptop", Price = 2500, CategoryId = 2 },
                new Product { ProductId = 3, ProductName = "Shirt", Price = 100, CategoryId = 3 }
            }.AsQueryable();

                var dbSetMock = new Mock<DbSet<Product>>();
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

                _mockContext.Setup(c => c.Products).Returns(dbSetMock.Object);

                // Act
                var result = await _repository.Get(null, null, null, 500, 2000, new int[] { });

                // Assert
                Assert.Equal(2, result.Count()); // "Phone" and "Laptop"
            }

            [Fact]
            public async Task Get_ReturnsFilteredByCategory()
            {
                // Arrange
                var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Phone", Price = 1000, CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Laptop", Price = 2500, CategoryId = 2 },
                new Product { ProductId = 3, ProductName = "Shirt", Price = 100, CategoryId = 3 }
            }.AsQueryable();

                var dbSetMock = new Mock<DbSet<Product>>();
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
                dbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

                _mockContext.Setup(c => c.Products).Returns(dbSetMock.Object);

                // Act
                var result = await _repository.Get(null, null, null, null, null, new int[] { 2 });

                // Assert
                Assert.Single(result);
                Assert.Equal("Laptop", result.First().ProductName);
            }

            [Fact]
            public async Task AddProduct_AddsProductAndReturnsIt()
            {
                // Arrange
                var newProduct = new Product { ProductName = "Tablet", Price = 1200, CategoryId = 1 };

                var dbSetMock = new Mock<DbSet<Product>>();
                _mockContext.Setup(c => c.Products).Returns(dbSetMock.Object);
                _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(1);

                // Act
                var result = await _repository.AddProduct(newProduct);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Tablet", result.ProductName);
                Assert.Equal(1200, result.Price);
                _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once());
            }
        }
    } }

