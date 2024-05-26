using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PRN221_GroupProject.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name {  get; set; }
        
        public bool Status { get; set; }
        
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        
        public virtual ICollection<CartHeader> CartHeaders { get; set; } = new List<CartHeader>();

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<EmailTemplate> EmailTemplates { get; set; } = new List<EmailTemplate>();

        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    }
}
