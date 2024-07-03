using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public Guid RoleGuid { get; set; }

    public virtual Department? Department { get; set; }
}
