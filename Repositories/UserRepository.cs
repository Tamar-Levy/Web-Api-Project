using System.Runtime.InteropServices;
using System.Text.Json;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;
public class UserRepository : IUserRepository
{
    MyShop215736745Context _context;

    public UserRepository(MyShop215736745Context context)
    {
        _context = context;
    }

    //Get
    public IEnumerable<string> Get()
    {
        return new string[] { };
    }

    // GetById
    public string GetById(int id)
    {
        return "";
    }

    //Login
    public async Task<User> LoginUser(string userName, string password)
    {
        User userFound= await _context.Users.FirstOrDefaultAsync(user => user.UserName == userName && user.Password == password);
        if (userFound != null)//תחזירי אותו בכל מקרה, אם הוא נאל יחזור לך נאל
            return userFound;
        return null;
    }

    //Register
    public async Task<User> Register(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    // Update
    public async Task<User> UpdateUser(int id, User userToUpdate)
    {
        _context.Users.Update(userToUpdate);
        await _context.SaveChangesAsync();
        return userToUpdate;
    }

}
