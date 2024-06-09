using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<CartHeader> CartHeaders { get; set; } = new List<CartHeader>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    public virtual ICollection<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();

    public virtual ICollection<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
