using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN221_GroupProject.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryId { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Type { get; set; } = null!;
    [Required]
    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
    [Required]
    public bool Status { get; set; }

    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
