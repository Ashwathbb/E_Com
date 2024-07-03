using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public decimal Price { get; set; }

    public Guid ProductGuid { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<UsersInfo> Users { get; set; } = new List<UsersInfo>();
}
