using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class OrderRepositoryUnitTesting
    {

        [Fact]
        public async Task AddOrder_Should_UpdateOrderSum_WhenSumChanges()
        {
            var products = new List<Product>
            {
                new Product { ProductId = 1, Price = 40 },
                new Product { ProductId = 2, Price = 20 }
            };

            var orders = new List<Order>
            {
                new Order
                {
                    UserId = 1,
                    OrderSum = 100,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 1, Quantity = 2 },
                        new OrderItem { ProductId = 2, Quantity = 1 }
                    }
                }
            };

            var mockContext = new Mock<MyShop215736745Context>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);
            mockContext.Setup(x => x.Orders).ReturnsDbSet(orders);
            mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var productRepository = new ProductsRepository(mockContext.Object);
            var mockLogger = new Mock<ILogger<OrdersRepository>>();
            var ordersRepository = new OrdersRepository(mockContext.Object, mockLogger.Object);

            var result = await ordersRepository.AddOrder(orders[0]);
            Assert.Equal(result, orders[0]);
        }

        [Fact]
        public async Task AddOrder_Should_LogWarning_WhenOrderSumChanges()
        {
        //    // Arrange
        //    var orderItems = new List<OrderItem>
        //{
        //    new OrderItem { ProductId = 1, Quantity = 1 }
        //};

        //    var order = new Order { OrderItems = orderItems, OrderSum = 300 }; // Expected sum is 100, but provided sum is 300

        //    var productsFromDb = new List<Product>
        //{
        //    new Product { ProductId = 1, Price = 100 }
        //};

        //    _mockProductDbSet.Setup(p => p.Where(It.IsAny<System.Linq.Expressions.Expression<System.Func<Product, bool>>>()))
        //        .Returns(productsFromDb.Where(p => orderItems.Select(oi => oi.ProductId).Contains(p.ProductId)).AsQueryable().BuildMockDbSet().Object);

        //    _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()))
        //        .ReturnsAsync(1);

        //    // Act
        //    var result = await _orderRepository.AddOrder(order);

        //    // Assert
        //    Assert.Equal(100, result.OrderSum);
        //    _mockLogger.Verify(logger => logger.LogWarning(It.IsAny<string>()), Times.Once);
        }
    }
}

