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
                var comments = Request.Form["comment"]; //Get rich editor value
                emailTemplate.Body = !string.IsNullOrEmpty(comments) ? comments : "";
                emailTemplate.CreatedDate = DateTime.Now;
                emailTemplate.CreatedBy = "Current User";
                _dbContext.EmailTemplates.Add(emailTemplate);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToPage("Index");
        }
    }
}
