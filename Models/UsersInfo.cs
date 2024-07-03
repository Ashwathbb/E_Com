using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class UsersInfo
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? EmailId { get; set; }

    public int? CountryId { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public string? FailedLoginAttempts { get; set; }

    public Guid UsersInfoGuid { get; set; }

    public Guid? DepartmentGuid { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
