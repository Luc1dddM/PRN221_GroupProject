using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PRN221_GroupProject.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Name can only contain letters and numbers")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        
        public virtual ICollection<CartHeader> CartHeaders { get; set; } = new List<CartHeader>();

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    }
}
