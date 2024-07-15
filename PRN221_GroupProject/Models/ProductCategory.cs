using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN221_GroupProject.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string ProductCategoryId { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    [Range(0, int.MaxValue, ErrorMessage = "The quantity must be greater than or equal to 0")]
    public int Quantity { get; set; }

    public bool Status { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Updatedby { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
