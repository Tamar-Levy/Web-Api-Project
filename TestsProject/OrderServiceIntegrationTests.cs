using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Repositories;
using Services;

namespace TestsProject
{
    public class OrderServiceIntegrationTests
    {
        private readonly ProductsRepository _productsRepository;
        private readonly OrdersService _orderService;
        private readonly DbContextOptions<AppDbContext> _dbOptions;

        public OrderServiceIntegrationTests()
        {
            // יצירת מסד נתונים In-Memory
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var context = new AppDbContext(_dbOptions);
            _productsRepository = new ProductsRepository(context);
            _orderService = new OrdersService(_productsRepository);

            // אתחול מסד הנתונים עם נתונים לדוגמה
            SeedDatabase(context);
        }

        private void SeedDatabase(AppDbContext context)
        {
            context.Products.AddRange(new List<Product>
        {
            new Product { ProductId = 1, Price = 100 },
            new Product { ProductId = 2, Price = 200 },
            new Product { ProductId = 3, Price = 300 }
        });

            context.SaveChanges();
        }

        [Fact]
        public async Task CheckOrderSum_ReturnsCorrectTotalSum_WhenProductsExist()
        {
            // Arrange
            var orderItems = new List<OrderItem>
        {
            new OrderItem { ProductId = 1 },
            new OrderItem { ProductId = 2 }
        };

            // Act
            var result = await _orderService.CheckOrderSum(orderItems);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(300, result);
        }

        [Fact]
        public async Task CheckOrderSum_ReturnsZero_WhenNoProductsInOrder()
        {
            // Arrange
            var orderItems = new List<OrderItem>();

            // Act
            var result = await _orderService.   CheckOrderSum(orderItems);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task CheckOrderSum_IgnoresNonExistingProducts()
        {
            // Arrange
            var orderItems = new List<OrderItem>
        {
            new OrderItem { ProductId = 99 } // מוצר שאינו קיים
        };

            // Act
            var result = await _orderService.CheckOrderSum(orderItems);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result);
        }
    
}
}
