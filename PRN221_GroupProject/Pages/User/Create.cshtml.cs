using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Users;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class Create : PageModel
{
    private readonly IUserRepository _userRepository;

    public Create(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The phone number must be numeric and exactly 10 digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

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
        public string Role { get; set; }

        [Required]
        public bool Status { get; set; }
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var existingUser = await _userRepository.FindUserByEmailAsync(Input.Email);
            if (existingUser != null)
            {
                TempData["error"] = "The email is already taken. Please use a different email.";
                return Page();
            }

            var result = await _userRepository.CreateUserAsync(Input);

            if (result.Succeeded)
            {
                TempData["success"] = "Create user successfully";
                return RedirectToPage("./Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
        catch
        {
            TempData["error"] = "Create user fail";
            return Page();
        }
    }
}
