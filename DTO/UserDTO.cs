using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public record GetUserDTO([EmailAddress] string UserName,[StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)] string FirstName, [StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)] string LastName);

    public record RegisterUserDTO([EmailAddress] string UserName, [StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)] string FirstName, [StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)] string LastName, string Password);

    public record LoginUserDTO([EmailAddress] string UserName);

}
