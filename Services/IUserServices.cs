using Entities;

namespace Services
{
    public interface IUserServices
    {
        Task<User> LoginUser(string userName, string password);
        Task<User> RegisterUser(User user);
        Task<User> UpdateUser(int id, User user);
        int CheckPassword(string password);
    }
}