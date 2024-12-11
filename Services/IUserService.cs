using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<User> LoginUser(string userName, string password);
        Task<User> GetById(int id);
        Task<User> RegisterUser(User user);
        Task<User> UpdateUser(int id, User user);
        int CheckPassword(string password);
    }
}