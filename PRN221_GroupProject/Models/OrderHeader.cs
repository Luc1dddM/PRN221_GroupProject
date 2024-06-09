using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class OrderHeader
{
    public int Id { get; set; }

    public string OrderHeaderId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Ward { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public string OrderStatus { get; set; } = null!;

    public double TotalPrice { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? CouponId { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual AspNetUser User { get; set; } = null!;
}
