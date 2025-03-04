using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;

namespace TestsProject
{
    public class UserRepositoryUnitTesting
    {
        [Fact]
        public async void Login_Validcredentials_ReturnsUser()
        {
            //Arange
            var user = new User { FirstName = "Naama", Password = "n0583265557@gmail.com" };

            var mockContext = new Mock<MyShop215736745Context>();
            var users=new List<User>() { user };
            mockContext.Setup(x=>x.Users).ReturnsDbSet(users);

            var userRepository=new UsersRepository(mockContext.Object);

            //Act
            var result=await userRepository.LoginUser(user.UserName,user.Password);

            //Assert
            Assert.Equal(user,result);
        }


    }
}