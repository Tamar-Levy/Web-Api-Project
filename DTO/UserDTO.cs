namespace DTO
{
    public record UserDTO(string UserName, string FirstName, string LastName, List<DateOnly?> OrdersOrderDate);
}
