using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository;

namespace MyApp.Namespace
{
    [Authorize(Policy = "admin")]
    [BindProperties]
    public class DetailModel : PageModel
    {
        private readonly IEmailRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailModel(IEmailRepository repository, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _repository = repository;
        }
        public EmailTemplate emailTemplate { get; set; }
        public List<string> categories { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            emailTemplate = await _repository.GetEmailTemplateById(id);
            if (emailTemplate == null)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var comments = Request.Form["comment"]; //Get rich editor value
                emailTemplate.Body = !string.IsNullOrEmpty(comments) ? comments : "";
                emailTemplate.UpdatedDate = DateTime.Now;
                emailTemplate.UpdatedBy = _userManager.GetUserId(User);

                // ModelState.ClearValidationState(nameof(emailTemplate));
                // if (!TryValidateModel(emailTemplate, nameof(emailTemplate)))
                // {
                //     emailTemplate = await _repository.GetEmailTemplateById(emailTemplate.EmailTemplateId);
                //     TempData["error"] = "test";
                //     return Page();
                // }
                emailTemplate = await _repository.UpdateEmailTemplate(emailTemplate);
                TempData["success"] = "Update email template successfully";
                return Page();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                emailTemplate = await _repository.GetEmailTemplateById(emailTemplate.EmailTemplateId);
                return Page();
            }
        }
    }
}
