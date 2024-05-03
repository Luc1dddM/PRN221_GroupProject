using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class CartHeader
{
    public string CartId { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string CouponCode { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
