using System;
using System.Collections.Generic;

namespace GrocerySales.Abstractions.Entities;

public partial class Customer
{
    public Guid CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
