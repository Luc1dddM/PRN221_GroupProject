using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public double Discount { get; set; }

    public string Description { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;
}
