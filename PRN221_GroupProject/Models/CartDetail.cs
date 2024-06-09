using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class CartDetail
{
    public string Id { get; set; } = null!;

    public string CartDetailId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Count { get; set; }

    public string CarId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual CartHeader Cart { get; set; } = null!;

    public virtual CartHeader Car { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
