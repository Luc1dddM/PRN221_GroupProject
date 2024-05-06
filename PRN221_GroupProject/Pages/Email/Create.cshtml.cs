using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Pages.Email
{

    [BindProperties]
    public class AddModel : PageModel
    {
        private readonly Prn221GroupProjectContext _dbContext;
        public AddModel(Prn221GroupProjectContext Context)
        {
            _dbContext = Context;
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
                emailTemplate.CreatedBy = "Current User";

                ModelState.ClearValidationState(nameof(emailTemplate));
                if (TryValidateModel(emailTemplate, nameof(emailTemplate)))
                {
                    _dbContext.EmailTemplates.Add(emailTemplate);
                    _dbContext.SaveChanges();
                    TempData["success"] = "Create email template successfully";
                    return RedirectToPage("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }
    }
}
