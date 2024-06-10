using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository;

namespace PRN221_GroupProject.Pages.Email
{
    [Authorize(Policy = "admin")]
    [BindProperties]
    public class AddModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailRepository _emailRepository;

        public AddModel(UserManager<ApplicationUser> userManager, IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
            _userManager = userManager;
        }
        public EmailTemplate emailTemplate { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            try
            {
                //Put Value for require field not from form
                var comments = Request.Form["comment"]; //Get rich editor value
                emailTemplate.Body = !string.IsNullOrEmpty(comments) ? comments : "";
                emailTemplate.CreatedDate = DateTime.Now;
                emailTemplate.CreatedBy = _userManager.GetUserId(User);
                _emailRepository.AddEmailTemplate(emailTemplate);
                TempData["success"] = "Create email template successfully";
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }
    }
}
