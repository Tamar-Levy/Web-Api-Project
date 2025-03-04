using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestsProject
{
    public class CategoryRepositoryUnitTesting
    {
 
             private readonly Mock<MyShop215736745Context> _mockContext;
            private readonly CategoriesRepository _repository;

            public CategoryRepositoryUnitTesting()
            {
                _mockContext = new Mock<MyShop215736745Context>();
                _repository = new CategoriesRepository(_mockContext.Object);
            }

            [Fact]
            public async Task Get_ReturnsAllCategories_WithProducts()
            {
                // Arrange
                List<Category> categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Electronics", Products = new List<Product> { new Product { ProductId = 1, ProductName = "Phone" } } },
                new Category { CategoryId = 2, CategoryName = "Clothing", Products = new List<Product> { new Product { ProductId = 2, ProductName = "Shirt" } } }
            };

                var dbSetMock = new Mock<DbSet<Category>>();
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(categories.AsQueryable().Provider);
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.AsQueryable().Expression);
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.AsQueryable().ElementType);
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

            object value = _mockContext.Setup(c => c.Categories).Returns(dbSetMock.Object);

            // Act
            var result = await _repository.Get();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());
                Assert.Equal("Electronics", result.First().CategoryName);
                Assert.Single(result.First().Products);
            }

            [Fact]
            public async Task GetById_ReturnsCategory_WhenCategoryExists()
            {
                // Arrange
                var category = new Category { CategoryId = 1, CategoryName = "Electronics", Products = new List<Product> { new Product { ProductId = 1, ProductName = "Phone" } } };
                var dbSetMock = new Mock<DbSet<Category>>();
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(new List<Category> { category }.AsQueryable().Provider);
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(new List<Category> { category }.AsQueryable().Expression);
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(new List<Category> { category }.AsQueryable().ElementType);
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(new List<Category> { category }.GetEnumerator());

            _mockContext.Setup(c => c.Categories).Returns(dbSetMock.Object);

            // Act
            var result = await _repository.GetById(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.CategoryId);
                Assert.Equal("Electronics", result.CategoryName);
            }

            [Fact]
            public async Task GetById_ReturnsNull_WhenCategoryDoesNotExist()
            {
                // Arrange
                var dbSetMock = new Mock<DbSet<Category>>();
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(new List<Category>().AsQueryable().Provider);
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(new List<Category>().AsQueryable().Expression);
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(new List<Category>().AsQueryable().ElementType);
                dbSetMock.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(new List<Category>().GetEnumerator());

            _mockContext.Setup(c => c.Categories).Returns(dbSetMock.Object);

            // Act
            var result = await _repository.GetById(999); // Category ID that doesn't exist

                // Assert
                Assert.Null(result);
            }
        }
    }



