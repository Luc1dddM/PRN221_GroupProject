using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class AspNetUserRole
{
    public string UserId { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public virtual AspNetRole Role { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
