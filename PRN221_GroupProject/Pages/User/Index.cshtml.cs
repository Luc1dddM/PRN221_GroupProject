using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Models.DTO;
using PRN221_GroupProject.Repository.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN221_GroupProject.Pages.User
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public IndexModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IList<UserListDTO> Users { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string SearchTerm { get; set; }

        public string[] statuses { get; set; }
        /*public string[] roles { get; set; }*/

        public async Task<IActionResult> OnGetAsync(string[] statusesParam, string[] rolesParam, string searchTermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            try
            {
                PageSize = pageSizeParam;
                PageNumber = pageNumberParam;
                SearchTerm = searchTermParam;

                statuses = statusesParam;
                /*roles = rolesParam;*/

                var result = await _userRepository.GetUsersAsync(statusesParam, rolesParam, SearchTerm, PageNumber, PageSize);
                Users = result.Users;
                TotalPages = result.totalPages;

                if (PageNumber < 1 || (PageNumber > TotalPages && TotalPages > 0))
                {
                    return RedirectToPage(new { pageNumberParam = 1, pageSizeParam = pageSizeParam, searchTermParam });
                }
            }
            catch
            {
                TempData["error"] = "You don't have access";
            }
            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(string id, string searchTermParam = "", int pageNumberParam = 1, int pageSizeParam = 10)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.FindUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userRepository.DeleteUserAsync(user);

            if (result.Succeeded)
            {
                TempData["success"] = "User deleted successfully";
                return RedirectToPage(new { pageNumberParam, pageSizeParam, searchTermParam });
            }
            else
            {
                TempData["error"] = "Error deleting user";
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }
    }
}
