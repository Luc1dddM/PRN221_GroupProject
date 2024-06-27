using System.Text;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository;

namespace MyApp.Namespace
{
    [Authorize(Policy = "admin")]
    public class indexModel : PageModel
    {
        public IEmailRepository _emailRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public List<EmailTemplate> emailTemplates { get; set; }
        public string[] categories { get; set; }
        public string[] statuses { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public string searchtearm { get; set; }

        [BindProperty]
        public string emailTemplateId { get; set; }

        [BindProperty]
        public string couponId { get; set; }

        public indexModel(IEmailRepository emailRepository, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _emailRepo = emailRepository;
        }

        public IActionResult OnGet(string[] statusesParam, string[] categoriesParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            categories = categoriesParam;
            statuses = statusesParam;
            searchtearm = searchtermParam;

            var emailPagination = _emailRepo.GetList(statusesParam, categoriesParam, searchtermParam, pageNumberParam, pageSizeParam);
            emailTemplates = emailPagination.listEmail;
            TotalPages = emailPagination.totalPages;

            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, categories = categoriesParam });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUploadExcel(IFormFile excelFile)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (excelFile != null && excelFile.Length > 0)
            {
                await _emailRepo.ImportEmailTemplates(excelFile, _userManager.GetUserId(User));
            }
            return Redirect("/admin/email");
        }
    }
}
