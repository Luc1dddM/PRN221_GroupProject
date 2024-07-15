using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN221_GroupProject.Models;

public partial class Coupon
{
    public int Id { get; set; }

    public string CouponId { get; set; } = null!;
    [Required(ErrorMessage = "This field is required.")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Coupon code must contain only letters and numbers.")]
    public string CouponCode { get; set; } = null!;
    [Required]
    [Range(1, 100, ErrorMessage = "Discount amount must be between 1 and 100.")]
    public double DiscountAmount { get; set; }

    public bool Status { get; set; }
    public double? MinAmount { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Max amount must be greater than Min amount.")]
    public double? MaxAmount { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();
}
