using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsProject
{
    public class OrderRepositoryIntegrationTest
    {
        private readonly DBFixture _dbFixture;
        public OrderRepositoryIntegrationTest()
        {
            _dbFixture = new ();
        }

        [Fact]
        public async Task AddOrder_Should_Update_OrderSum_When_SumChanges()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<OrdersRepository>>();
            var repository = new OrdersRepository(_dbFixture.Context, loggerMock.Object);
            var productRepository = new ProductsRepository(_dbFixture.Context);

            // הגדרת רשימת פריטים עם מוצרים
            var orderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 1 },
                new OrderItem { ProductId = 2, Quantity = 2 }
            };

            // יצירת הזמנה עם סכום לא נכון
            var order = new Order
            {
                OrderItems = orderItems,
                OrderSum = 300 // סכום שלא תואם לסכום הצפוי
            };
            var categoriesFromDb = new List<Category>
            {
             new Category {CategoryName = "Electronics" },
             new Category {CategoryName = "Clothing" }
            };

            foreach (var category in categoriesFromDb)
            {
                // הוסף את הקטגוריות למסד הנתונים
                await _dbFixture.Context.Categories.AddAsync(category);
            }

            await _dbFixture.Context.SaveChangesAsync();
            // הגדרת מוצרים עם מחירים וקטגוריות - ללא הגדרת ProductId
            var productsFromDb = new List<Product>
            {
                new Product { CategoryId = 1, Price = 100 },
                new Product { CategoryId = 2, Price = 200 }
            };

            // הוספת מוצרים למסד הנתונים
            var dbProduct1 = await productRepository.AddProduct(productsFromDb[0]);
            var dbProduct2 = await productRepository.AddProduct(productsFromDb[1]);

            // Act
            var result = await repository.AddOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.OrderSum); // 100 + 200 * 2 = 500
            _dbFixture.Dispose();
        }



        [Fact]
        public async Task AddOrder_Should_LogWarning_When_OrderSumChanges()
        {
        //    // Arrange
        //    var repository = new OrdersRepository(_dbFixture.Context, _dbFixture.Logger);

        //    var orderItems = new List<OrderItem>
        //{
        //    new OrderItem { ProductId = 1, Quantity = 1 }
        //};

        //    var order = new Order
        //    {
        //        OrderItems = orderItems,
        //        OrderSum = 300 // סכום שלא תואם לסכום הצפוי
        //    };

        //    var productsFromDb = new List<Product>
        //{
        //    new Product { ProductId = 1, Price = 100 }
        //};

        //    // Mocking the DbSet<Product> to return the correct products
        //    _dbFixture.MockProductDbSet(productsFromDb);

        //    // Act
        //    var result = await repository.AddOrder(order);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(100, result.OrderSum); // הסכום המחושב הוא 100
        //    _dbFixture.Logger.Verify(logger => logger.LogWarning(It.IsAny<string>()), Times.Once);
        //    _dbFixture.Dispose();
        }
    }
}
