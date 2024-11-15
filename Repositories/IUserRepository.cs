using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        IEnumerable<string> Get();
        string GetById(int id);
        User LoginUser(string userName, string password);
        User Register(User user);
        User UpdateUser(int id, User userToUpdate);
    }
}