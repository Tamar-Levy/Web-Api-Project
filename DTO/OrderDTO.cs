using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderDTO(int OrderId, DateOnly OrderDate, [Range(00.01, double.MaxValue, ErrorMessage = "המחיר חייב להיות לפחות 1")] int OrderSum, string UserUserName);

    public record OrderItemDTO(int ProductId, int Quantity);

    public record PostOrderDTO( DateOnly OrderDate,[Range(00.01, double.MaxValue, ErrorMessage = "המחיר חייב להיות לפחות 1")] int OrderSum, int UserId, List<OrderItemDTO> OrderItems );

}
