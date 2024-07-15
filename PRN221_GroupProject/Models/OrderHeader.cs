using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN221_GroupProject.Models;

public partial class OrderHeader
{
    public int Id { get; set; }

    public string OrderHeaderId { get; set; } = null!;

    [Required]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only has text characters are allowed.")]
    public string Name { get; set; } = null!;

    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "The phone number must be numeric and exactly 10 digits.")]
    public string Phone { get; set; } = null!;

    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z0-9\s./]+$", ErrorMessage = "The address can only contain letters, numbers, spaces, periods ('.'), and slashes ('/').")]
    public string Address { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;

    [Required]
    public string District { get; set; } = null!;

    [Required]
    public string Ward { get; set; } = null!;

    [Required]
    public string PaymentMethod { get; set; } = null!;

    [Required]
    public string OrderStatus { get; set; } = null!;

    [Required]
    public double TotalPrice { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? CouponId { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
