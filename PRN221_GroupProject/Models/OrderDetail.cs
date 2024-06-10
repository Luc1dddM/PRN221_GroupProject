using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public string OrderDetailId { get; set; } = null!;

    public string OrderHeaderId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Count { get; set; }

    public double Price { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual OrderHeader OrderHeader { get; set; } = null!;
}
