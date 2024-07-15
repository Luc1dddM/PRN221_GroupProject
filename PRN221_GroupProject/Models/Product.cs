using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN221_GroupProject.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [Range(0,double.MaxValue, ErrorMessage = "The Price must be greater than or equal to 0")]
    public double Price { get; set; }

    [Required]
    public string? Description { get; set; }
    [Required]
    public string? ImageUrl { get; set; }
    [Required]
    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
    [Required]
    public bool Status { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
