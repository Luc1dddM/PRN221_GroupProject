using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public string OrderDetailId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Count { get; set; }

    public string Color { get; set; } = null!;

    public double Price { get; set; }

    public string OrderHeaderId { get; set; } = null!;

    public virtual OrderHeader OrderHeader { get; set; } = null!;
}
