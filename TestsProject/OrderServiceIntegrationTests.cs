using Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsProject
{
    public class OrderserviceIntegrationTests
    {
        public class OrderServiceIntegrationTest : IClassFixture<DBFixture>
        {
            private readonly MyShop215736745Context _context;
            private readonly OrdersService _service;
            public OrderServiceIntegrationTest(DBFixture fixture)
            {
                _context = fixture.Context;
                var mockLogger = new Mock<ILogger<OrdersService>>();
                _service = new OrdersService(new OrdersRepository(_context),mockLogger.Object, new ProductsRepository(_context));
            }
            [Fact]
            public async Task Post_ShouldSaveOrder_WithCorrectTotalAmount()
            {
                // Arrange: Create a category for products (because of Foreign Key)
                var category = new Category { CategoryName = "Electronics" };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                // Create products with valid category
                var product1 = new Product { ProductName = "Laptop", Price = 1000, Image = "laptop.jpg", Category = category };
                var product2 = new Product { ProductName = "Phone", Price = 500, Image = "phone.jpg", Category = category };
                var product3 = new Product { ProductName = "Tablet", Price = 300, Image = "tablet.jpg", Category = category };

                _context.Products.AddRange(product1, product2, product3);
                await _context.SaveChangesAsync();

                // Create user (because UserId is required in order)
                var user = new User { UserName = "test@example.com", Password = "geyt6gG", FirstName = "John", LastName = "Doe" };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Create order with valid products and UserId
                var order = new Order
                {
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    OrderSum = 1800,  // This should be calculated by checkSum
                    UserId = user.UserId,
                    OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = product1.ProductId, Quantity = 1 },
                    new OrderItem { ProductId = product2.ProductId, Quantity = 1 },
                    new OrderItem { ProductId = product3.ProductId, Quantity = 1 }
                }
                };

                // Act: Send the order to the function being tested
                var savedOrder = await _service.AddOrder(order);

                // Assert: Check data validity
                Assert.NotNull(savedOrder);
                Assert.Equal(1800, savedOrder.OrderSum); // 1000 + (500*2) + 300 = 1800
                Assert.Equal(3, savedOrder.OrderItems.Count); // 3 products in order
                Assert.Equal(user.UserId, savedOrder.UserId); // Check that user is associated with order
            }

            [Fact]
            public async Task Post_ShouldSaveOrder_WithUnCorrectTotalAmount()
            {
                // Arrange: Create a category for products (because of Foreign Key)
                var category = new Category { CategoryName = "Electronics" };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                // Create products with valid category
                var product1 = new Product { ProductName = "Laptop", Price = 1000, Image = "laptop.jpg", Category = category };
                var product2 = new Product { ProductName = "Phone", Price = 500, Image = "phone.jpg", Category = category };
                var product3 = new Product { ProductName = "Tablet", Price = 300, Image = "tablet.jpg", Category = category };

                _context.Products.AddRange(product1, product2, product3);
                await _context.SaveChangesAsync();

                // Create user (because UserId is required in order)
                var user = new User { UserName = "test@example.com", Password = "geyt6gG", FirstName = "John", LastName = "Doe" };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Create order with valid products and UserId
                var order = new Order
                {
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    OrderSum = 100,  // This should be calculated by checkSum
                    UserId = user.UserId,
                    OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = product1.ProductId, Quantity = 1 },
                    new OrderItem { ProductId = product2.ProductId, Quantity = 1 },
                    new OrderItem { ProductId = product3.ProductId, Quantity = 1 }
                }
                };

                // Act: Send the order to the function being tested
                var savedOrder = await _service.AddOrder(order);

                // Assert: Check data validity
                Assert.NotNull(savedOrder);
                Assert.Equal(1800, savedOrder.OrderSum); // 1000 + (500*2) + 300 = 1800
                Assert.Equal(3, savedOrder.OrderItems.Count); // 3 products in order
                Assert.Equal(user.UserId, savedOrder.UserId); // Check that user is associated with order
            }
        }
    }
}
