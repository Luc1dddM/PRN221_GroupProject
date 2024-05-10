using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public bool Status { get; set; }
}
