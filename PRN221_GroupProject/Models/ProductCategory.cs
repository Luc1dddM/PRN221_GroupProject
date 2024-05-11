using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class ProductCategory
{
    public string CategoryId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int? Quantity { get; set; }

    public bool Status { get; set; }

    public int Id { get; set; }

    public string ProductCategoryId { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
