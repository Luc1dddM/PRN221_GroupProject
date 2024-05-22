using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ApplicationUser? appUser;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        public void OnGet()
        {
            var task = userManager.GetUserAsync(User);
            task.Wait();
            appUser = task.Result;
        }
    }
}
