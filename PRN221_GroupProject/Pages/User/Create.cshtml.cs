using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models; 
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class Create : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager; 
    private readonly RoleManager<IdentityRole> _roleManager;

    public Create(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only text characters are allowed.")]
        public string Name { get; set; }

        // contains only numbers and does not exceed 10 digits
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The phone number must be numeric and exactly 10 digits.")]
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

        [Required(ErrorMessage = "Please select a role.")]
        public string Role {  get; set; }

        [Required]
        public bool Status { get; set; }
    }


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Check if the form is valid
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(Input.Email);
            if (existingUser != null)
            {
                TempData["error"] = "The email is already taken. Please use a different email.";
                return Page();
            }

            var user = new ApplicationUser
            {
                Name = Input.Name,
                UserName = Input.Email,
                Email = Input.Email,
                PhoneNumber = Input.PhoneNumber,
                Status = Input.Status,
                EmailConfirmed = true // Set email as confirmed
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Input.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Input.Role));
                }

                await _userManager.AddToRoleAsync(user, Input.Role);

                TempData["success"] = "Create user successfully";

                return RedirectToPage("./Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            /*TempData["success"] = "Create user successfully";*/
            return Page();
        }
        catch 
        {
            TempData["error"] = "Create user fail";
        }
        return Page();
    }
}
