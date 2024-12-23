namespace DTO
{
    public record GetUserDTO(string UserName, string FirstName, string LastName, List<OrderDTO> Orders);

    public record RegisterUserDTO(string UserName, string FirstName, string LastName,string Password);

}
