using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public Guid CountryGuid { get; set; }

    public virtual ICollection<UsersInfo> UsersInfos { get; set; } = new List<UsersInfo>();
}
