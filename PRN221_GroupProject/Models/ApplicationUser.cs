using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PRN221_GroupProject.Models
{
    public partial class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name {  get; set; }

        public bool Status { get; set; }
    }
}
