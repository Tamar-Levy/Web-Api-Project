using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record CategoryDTO(int categoryId, [StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)] string categoryName);
}
