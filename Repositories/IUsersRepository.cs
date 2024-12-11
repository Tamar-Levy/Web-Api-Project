using Entities;

namespace Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetById(int id);
        Task<User> LoginUser(string userName, string password);
        Task<User> Register(User user);
        Task<User> UpdateUser(int id, User userToUpdate);
    }
}