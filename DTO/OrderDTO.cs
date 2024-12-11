using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record OrderDTO(DateOnly OrderDate, int OrderSum,string UserUserName);
}
