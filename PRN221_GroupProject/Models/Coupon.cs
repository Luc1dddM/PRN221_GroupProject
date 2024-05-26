using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class Coupon
{
    public int Id { get; set; }

    public string CouponId { get; set; } = null!;

    public string CouponCode { get; set; } = null!;

    public double DiscountAmount { get; set; }

    public double? MinAmount { get; set; }

    public double? MaxAmount { get; set; }

    public virtual ICollection<CartHeader> CartHeaders { get; set; } = new List<CartHeader>();
}
