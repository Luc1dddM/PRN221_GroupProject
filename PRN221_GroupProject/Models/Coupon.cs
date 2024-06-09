using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class Coupon
{
    public int Id { get; set; }

    public string CouponId { get; set; } = null!;

    public string CouponCode { get; set; } = null!;

    public double DiscountAmount { get; set; }

    public bool Status { get; set; }

    public double? MinAmount { get; set; }

    public double? MaxAmount { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual AspNetUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();
}
