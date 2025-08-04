using System;
using System.Collections.Generic;

namespace GrocerySales.Abstractions.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string? Username { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public DateOnly? DayOfBirth { get; set; }
    
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public string? Address { get; set; }

    public bool? Status { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<ExternalLogin> ExternalLogins { get; set; } = new List<ExternalLogin>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}
