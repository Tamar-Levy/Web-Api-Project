using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities; 

public partial class User
{
    public int UserId { get; set; }

    [EmailAddress]
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    [StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)]
    public string? FirstName { get; set; }

    [StringLength(20, ErrorMessage = "Name can be beteen 2 till 20", MinimumLength = 2)]
    public string? LastName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
