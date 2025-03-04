using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
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
    }
}