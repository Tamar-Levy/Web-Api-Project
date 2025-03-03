using Repositories;
using Entities;
using Zxcvbn;
namespace Services;

public class UserService : IUserService
{
    IUsersRepository _userRepository;
    public UserService(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> LoginUser(string userName, string password)
    {
        return await _userRepository.LoginUser(userName, password);
    }

    public async Task<User> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<User> RegisterUser(User user)
    {
        var checkPasswordResult = CheckPassword(user.Password);
        if (checkPasswordResult < 2)
        {
            throw new Exception("Weak password");
        }
        return await _userRepository.Register(user);
    }

    public async Task<User> UpdateUser(int id, User user)
    {
        var result = CheckPassword(user.Password);
        if (result < 2)
        {
            throw new Exception("Weak password");
        }
        return await _userRepository.UpdateUser(id, user);
    }
   
    public int CheckPassword(string password)
    {
        return Zxcvbn.Core.EvaluatePassword(password).Score;
    }
}
