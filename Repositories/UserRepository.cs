using System.Runtime.InteropServices;
using System.Text.Json;
using Entities;
namespace Repositories;
public class UserRepository : IUserRepository
{
    string filePath = "M:/MyShop/MyShop/Users.txt";
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


    public User LoginUser(string userName, string password)
    {
        using (StreamReader reader = System.IO.File.OpenText(filePath))
        {
            string? currentUserInFile;
            while ((currentUserInFile = reader.ReadLine()) != null)
            {
                User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                if (user.UserName == userName && user.Password == password)
                    return user;
            }
        }
        return null;


    }

    //Register
    public User Register(User user)
    {
        int numberOfUsers = System.IO.File.ReadLines(filePath).Count();
        user.UserId = numberOfUsers + 1;
        string userJson = JsonSerializer.Serialize(user);
        try
        {
            System.IO.File.AppendAllText(filePath, userJson + Environment.NewLine);
        }
        catch
        {
            return null;
        }
        return user;
    }

    // Update
    public User UpdateUser(int id, User userToUpdate)
    {
        string textToReplace = string.Empty;
        using (StreamReader reader = System.IO.File.OpenText(filePath))
        {
            string currentUserInFile;
            while ((currentUserInFile = reader.ReadLine()) != null)
            {

                User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                if (user.UserId == id)
                    textToReplace = currentUserInFile;
            }
        }

        if (textToReplace != string.Empty)
        {
            string text = System.IO.File.ReadAllText(filePath);
            text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
            System.IO.File.WriteAllText(filePath, text);
            return userToUpdate;
        }
        return null;
    }

}
