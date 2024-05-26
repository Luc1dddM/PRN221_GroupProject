using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PRN221_GroupProject.Pages.User
{
    public class CreateUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "The email field is not a valid e-mail address.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            // contains only numbers and does not exceed 11 digits
            [Required]
            [RegularExpression(@"^\d{1,11}$", ErrorMessage = "The phone number must be numeric and up to 11 digits.")]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            // at least one capital letter, one number, and one special character.
            [Required]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{6,50}$", ErrorMessage = "The password must be between 6 and 50 characters long and have at least one uppercase letter, one number, and one special character.")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    PhoneNumber = Input.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
