using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public record GetUserDTO(int UserId, [EmailAddress] string UserName,[StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)] string FirstName, [StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)] string LastName);

    public record RegisterUserDTO([EmailAddress] string UserName, [StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)] string FirstName, [StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)] string LastName, string Password);

}
