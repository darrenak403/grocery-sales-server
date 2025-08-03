using System;
using System.Collections.Generic;

namespace GrocerySales.Abstractions.Entities;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string? Barcode { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
