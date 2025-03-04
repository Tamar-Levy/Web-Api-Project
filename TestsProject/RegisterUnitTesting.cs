using Entities;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Repositories;
namespace TestsProject
{
    public class RegisterUnitTesting
    {
        private readonly Mock<IUsersRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public RegisterUnitTesting()
        {
            _userRepositoryMock = new Mock<IUsersRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task RegisterUser_ShouldRegisterUser_WhenPasswordIsStrong()
        {
            // Arrange
            var user = new User { UserName = "testUser", Password = "StrongP@ssword123" };
            _userRepositoryMock.Setup(repo => repo.Register(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.RegisterUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("testUser", result.UserName);
            _userRepositoryMock.Verify(repo => repo.Register(user), Times.Once);
        }

        [Fact]
        public async Task RegisterUser_ShouldThrowException_WhenPasswordIsWeak()
        {
            // Arrange
            var user = new User { UserName = "testUser", Password = "123" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.RegisterUser(user));
            Assert.Equal("Weak password", exception.Message);

            _userRepositoryMock.Verify(repo => repo.Register(It.IsAny<User>()), Times.Never);
        }
    }

}
}
