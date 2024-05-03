using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;

namespace MyApp.Namespace
{
    public class indexModel : PageModel
    {
        private readonly Prn221GroupProjectContext _dbContext;
        public List<EmailTemplate> emailTemplates{ get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public indexModel(Prn221GroupProjectContext dbContext){
            _dbContext = dbContext;
        }
        public IActionResult  OnGet(int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;

            var totalItems = _dbContext.EmailTemplates.Count();
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            if (pageNumber < 1 || pageNumber > TotalPages)
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize });
            }

            emailTemplates = _dbContext.EmailTemplates.Skip((pageNumber - 1) * pageSize)
                                                      .Take(pageSize)
                                                      .ToList();

            return Page();
        }
    }
}
