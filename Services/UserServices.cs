using Repositories;
using Entities;
using Zxcvbn;
namespace Services;

public class UserServices : IUserServices
{
    IUserRepository _userRepository;
    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public User LoginUser(string userName, string password)
    {
        return _userRepository.LoginUser(userName, password);
    }
    public User RegisterUser(User user)
    {
        var result = CheckPassword(user.Password);
        if (result< 2)
        {
            User tmpUser = new();
            tmpUser.FirstName = "Weak password";
            return tmpUser;
        }
        return _userRepository.Register(user);
    }
    public User UpdateUser(int id, User user)
    {
        var result = CheckPassword(user.Password);
        if (result < 2)
        {
            User tmpUser = new();
            tmpUser.FirstName = "Weak password";
            return tmpUser;
        }
        return _userRepository.UpdateUser(id, user);
    }
    public int CheckPassword(string password)
    {
        return Zxcvbn.Core.EvaluatePassword(password).Score;
    }
}
