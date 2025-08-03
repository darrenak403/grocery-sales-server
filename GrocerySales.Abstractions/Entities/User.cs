using System;
using System.Collections.Generic;

namespace GrocerySales.Abstractions.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public Guid RoleId { get; set; }

    public virtual ICollection<ExternalLogin> ExternalLogins { get; set; } = new List<ExternalLogin>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}
