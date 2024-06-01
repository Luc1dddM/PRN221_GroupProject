using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN221_GroupProject.Models;

public partial class Coupon :IValidatableObject
{
    public int Id { get; set; }

    public string CouponId { get; set; } = null!;

    [Required(ErrorMessage = "Coupon code is required.")]
    [RegularExpression(@"^([^""!'*\\]*)$", ErrorMessage = "Special Character is not allowed")]
    public string CouponCode { get; set; } = null!;


    
    public double DiscountAmount { get; set; }


    [Range(0, double.MaxValue, ErrorMessage = "Discount amount must be a non-negative number")]
    public double? MinAmount { get; set; }


    [Range(0, double.MaxValue, ErrorMessage = "Discount amount must be a non-negative number")]
    public double? MaxAmount { get; set; }

    public virtual ICollection<CartHeader> CartHeaders { get; set; } = new List<CartHeader>();

    public IEnumerable<ValidationResult> Validate (ValidationContext ValidationContext)
    {
        if (DiscountAmount == null)
        {
            yield return new ValidationResult("Cannot be null");
        }
    }
}
