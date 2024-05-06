using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;

namespace MyApp.Namespace
{
    [BindProperties]
    public class DetailModel : PageModel
    {
        private readonly Prn221GroupProjectContext _dbContext;
        public DetailModel(Prn221GroupProjectContext Context){
            _dbContext = Context;
        }
        public EmailTemplate emailTemplate{ get; set; }
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
           var newEmailTemplate = _dbContext.EmailTemplates.FirstOrDefault(e => e.EmailTemplateId.Equals(emailTemplate.EmailTemplateId));
           if (newEmailTemplate == null){
                return RedirectToPage("/Error");
           }
            var comments = Request.Form["comment"]; //Get rich editor value
            newEmailTemplate.Active = emailTemplate.Active;
            newEmailTemplate.Subject = emailTemplate.Subject;
            newEmailTemplate.Description = emailTemplate.Description;
            newEmailTemplate.Category = emailTemplate.Category;
            newEmailTemplate.Body = !string.IsNullOrEmpty(comments) ? comments : "";
            newEmailTemplate.UpdatedBy = "Current User";
            newEmailTemplate.UpdatedDate = DateTime.Now;
            _dbContext.SaveChanges();
            return RedirectToPage("/email/detail", new { id = emailTemplate.EmailTemplateId });

        }
    }
}
