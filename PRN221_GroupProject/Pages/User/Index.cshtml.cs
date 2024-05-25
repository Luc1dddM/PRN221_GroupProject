using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN221_GroupProject.Pages.User
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; 

        public IndexModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) // Add RoleManager to constructor
        {
            _userManager = userManager;
            _roleManager = roleManager; 
        }

        public IList<(ApplicationUser User, IList<string> Roles)> Users { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchTermParam = "", int pageNumberParam = 1, int pageSizeParam = 10)
        {
            PageSize = pageSizeParam;
            PageNumber = pageNumberParam;
            SearchTerm = searchTermParam;

            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(u => u.UserName.Contains(SearchTerm) || u.Email.Contains(SearchTerm));
            }

            TotalPages = (int)Math.Ceiling(await query.CountAsync() / (double)PageSize);

            if (PageNumber < 1 || (PageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumberParam = 1, pageSizeParam, searchTermParam });
            }

            Users = new List<(ApplicationUser User, IList<string> Roles)>();

            var users = await _userManager.Users.ToListAsync();

            var combinedData = new List<(ApplicationUser User, IList<string> Roles)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                combinedData.Add((user, roles));
            }
            Users = combinedData;
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id, string searchTermParam = "", int pageNumberParam = 1, int pageSizeParam = 10)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                if (Users != null)
                {
                    // Delete successfully
                    var userToRemove = Users.FirstOrDefault(u => u.User.Id == id);
                    if (userToRemove != default)
                    {
                        Users.Remove(userToRemove);
                    }
                }
                // Reload page
                return RedirectToPage(new { pageNumberParam, pageSizeParam, searchTermParam });
            }
            else
            {
                // Delete fails
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }
    }
}
