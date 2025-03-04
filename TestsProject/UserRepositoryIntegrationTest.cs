using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Moq;
using Repositories;
using System;

namespace TestsProject
{
    public class UserRepositoryIntegrationTest
    {
        private readonly DBFixture _dbFixture;
        
        public UserRepositoryIntegrationTest()
        {
            _dbFixture = new ();
        }

        [Fact]
        public async Task CreateUser_Should_Add_User_To_Database()
        {
            // Arrange
            var repository = new UsersRepository(_dbFixture.Context);

            // Act
            var user = new User { FirstName = "Naama", LastName = "Shmuelevitz", UserName = "n0583265557@gmail.com", Password = "215736745" };
            var dbUser = await repository.Register(user);

            // Assert
            Assert.NotNull(dbUser);
            Assert.NotEqual(0, dbUser.UserId);
            Assert.Equal("n0583265557@gmail.com", dbUser.UserName);
            _dbFixture.Dispose();
        }

        [Fact]
        public async Task GetById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange: Create and save a user
            var repository = new UsersRepository(_dbFixture.Context);

            var user = new User
            {
                UserName = "getbyid@example.com",
                Password = "StrongPassword!123",
                FirstName = "John",
                LastName = "Doe"
            };
            _dbFixture.Context.Users.Add(user);
            await _dbFixture.Context.SaveChangesAsync();

            // Act: Retrieve the user by ID
            var retrievedUser = await repository.GetById(user.UserId);

            // Assert: Check that the retrieved user matches the saved user
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.UserId, retrievedUser.UserId);
            Assert.Equal(user.UserName, retrievedUser.UserName);
            Assert.Equal(user.FirstName, retrievedUser.FirstName);
            Assert.Equal(user.LastName, retrievedUser.LastName);
        }

        [Fact]
        public async Task UpdateUser_Should_Update_Existing_User()
        {
            // Arrange
            var repository = new UsersRepository(_dbFixture.Context);
            var user = new User { FirstName = "Initial", LastName = "User", UserName = "initial.user@example.com", Password = "initial123" };
            var dbUser = await repository.Register(user);

            // Act
            dbUser.FirstName = "Updated";
            dbUser.LastName = "User";
            dbUser.UserName = "updated.user@example.com";
            dbUser.Password = "updated123";
            var updatedUser = await repository.UpdateUser(dbUser.UserId, dbUser);

            // Assert
            Assert.NotNull(updatedUser);
            Assert.Equal(dbUser.UserId, updatedUser.UserId);
            Assert.Equal("Updated", updatedUser.FirstName);
            Assert.Equal("User", updatedUser.LastName);
            Assert.Equal("updated.user@example.com", updatedUser.UserName);
            _dbFixture.Dispose();
        }
    }
}