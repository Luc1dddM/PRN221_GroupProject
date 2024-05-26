using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class CartHeader
{
    public int Id { get; set; }

    public string CartId { get; set; } = null!;

    public string CouponCode { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
}
