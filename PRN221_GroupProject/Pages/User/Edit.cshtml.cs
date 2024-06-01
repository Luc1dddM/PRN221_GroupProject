using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class EditModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public EditModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only text characters are allowed.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The email field is not a valid e-mail address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

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

        // contains only numbers and does not exceed 10 digits
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The phone number must be numeric and exactly 10 digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public bool Status { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            TempData["error"] = "User ID is not provided.";
            return RedirectToPage("./Index");
        }

        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            TempData["error"] = "User not found.";
            return RedirectToPage("./Index");
        }

        Input = new InputModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Status = user.Status,
            Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
        };

        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var user = await _userManager.FindByIdAsync(Input.Id);

            user.Name = Input.Name;
            /*user.Email = Input.Email;*/
            user.PhoneNumber = Input.PhoneNumber;
            user.Status = Input.Status;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any() && currentRoles.First() != Input.Role)
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRoles.First());
                    if (!await _roleManager.RoleExistsAsync(Input.Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Input.Role));
                    }
                    await _userManager.AddToRoleAsync(user, Input.Role);
                }

                TempData["success"] = "User updated successfully";

                return RedirectToPage("./Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
        }

        return Page();
    }
}
