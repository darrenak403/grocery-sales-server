using System;
using System.Collections.Generic;

namespace GrocerySales.Abstractions.Entities;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid? CustomerId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
