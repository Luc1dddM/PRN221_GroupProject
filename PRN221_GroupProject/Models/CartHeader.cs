using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class CartHeader
{
    public string Id { get; set; } = null!;

    public string CartId { get; set; } = null!;

    public string CouponId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual ApplicationUser User { get; set; } = null!;
}
