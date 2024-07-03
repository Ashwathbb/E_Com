using System;
using System.Collections.Generic;

namespace Shop.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public Guid DepartmentGuid { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
