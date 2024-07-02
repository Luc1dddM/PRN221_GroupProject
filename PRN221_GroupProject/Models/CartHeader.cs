using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class CartHeader
{
    public int Id { get; set; }

    public string CartId { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;
}
