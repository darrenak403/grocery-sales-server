using System;
using System.Collections.Generic;

namespace GrocerySales.Abstractions.Entities;

public partial class ExternalLogin
{
    public Guid ExternalLoginId { get; set; }

    public Guid UserId { get; set; }

    public string Provider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
