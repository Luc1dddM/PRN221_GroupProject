using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;

namespace MyApp.Namespace
{
    [BindProperties]
    public class DetailModel : PageModel
    {
        private readonly Prn221GroupProjectContext _dbContext;
        public DetailModel(Prn221GroupProjectContext Context)
        {
            _dbContext = Context;
        }
        public EmailTemplate emailTemplate { get; set; }
        public List<string> categories { get; set; }
        public IActionResult OnGet(string id)
        {
            emailTemplate = _dbContext.EmailTemplates.FirstOrDefault(e => e.EmailTemplateId.Equals(id));
            if (emailTemplate == null)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                var comments = Request.Form["comment"]; //Get rich editor value
                emailTemplate.Body = !string.IsNullOrEmpty(comments) ? comments : "";
                emailTemplate.CreatedDate = DateTime.Now;
                emailTemplate.CreatedBy = "Current User";

                ModelState.ClearValidationState(nameof(emailTemplate));
                if (!TryValidateModel(emailTemplate, nameof(emailTemplate)))
                {
                    emailTemplate = _dbContext.EmailTemplates.FirstOrDefault(e => e.EmailTemplateId.Equals(emailTemplate.EmailTemplateId));
                    return Page();
                }

                var newEmailTemplate = _dbContext.EmailTemplates.FirstOrDefault(e => e.EmailTemplateId.Equals(emailTemplate.EmailTemplateId));
                if (newEmailTemplate == null)
                {
                    return RedirectToPage("/Error");
                }
                newEmailTemplate.Active = emailTemplate.Active;
                newEmailTemplate.Subject = emailTemplate.Subject;
                newEmailTemplate.Description = emailTemplate.Description;
                newEmailTemplate.Category = emailTemplate.Category;
                newEmailTemplate.Body = !string.IsNullOrEmpty(comments) ? comments : "";
                newEmailTemplate.Name = emailTemplate.Name;
                newEmailTemplate.UpdatedBy = "Current User";
                newEmailTemplate.UpdatedDate = DateTime.Now;
                _dbContext.SaveChanges();
                TempData["success"] = "Update email template successfully";
                emailTemplate = newEmailTemplate;
                return Page();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                emailTemplate = _dbContext.EmailTemplates.FirstOrDefault(e => e.EmailTemplateId.Equals(emailTemplate.EmailTemplateId));
                return Page();
            }


        }
    }
}
