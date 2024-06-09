using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Repository.Users;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class EditModel : PageModel
{
    private readonly IUserRepository _userRepository;

    public EditModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public string Id { get; set; }

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

        [Required(ErrorMessage = "Please select a role.")]
        public string Role { get; set; }

        [Required]
        public bool Status { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var user = await _userRepository.FindUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        Input = new InputModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Status = user.Status,
            Role = await _userRepository.GetUserRoleAsync(user)
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _userRepository.EditUserAsync(Input.Id, Input);

        if (result.Succeeded)
        {
            TempData["success"] = "User edited successfully";
            return RedirectToPage("./Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        TempData["error"] = "Failed to edit user";
        return Page();
    }
}
