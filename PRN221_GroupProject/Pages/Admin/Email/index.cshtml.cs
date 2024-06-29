using System.Text;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public string sortBy { get; set; }
        public string sortOrder { get; set; }
        public string currentSortBy { get; set; }

        [BindProperty]
        public string emailTemplateId { get; set; }

        [BindProperty]
        public string couponId { get; set; }

        public indexModel(IEmailRepository emailRepository, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _emailRepo = emailRepository;
        }

        public IActionResult OnGet(string[] statusesParam, string[] categoriesParam, bool keepSort = false, string currentSortByParam = "", string searchtermParam = "", string sortByParam = "", string sortOrderParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            //Set Params for current value of model
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            categories = categoriesParam;
            statuses = statusesParam;
            searchtearm = searchtermParam;


            //Change the Sort By and Sort Order for the next sort
            /*Đọc Kĩ Hướng Dẫn Sử Dụng Trước Khi Dùng!
            -Ý tưởng: Nếu sort hiện tại là sort theo name người dùng sort name lần nữa=> SortBy Sẽ thay đổi (asc => desc => no sort)
            -Mình sẽ thay đổi sortBy để truyền qua bên Index 
            Ví dụ: hiện tại biến SortBy đang là Name, SortOrder đang là asc => Lần đầu người dùng chọn sort by name
            Vậy sẽ có 3 trường hợp: 
                1: Giữ Nguyên sort (người dùng chuyển trang, filter, ...)
                2: Người dùng chọn sort bằng trường khác: category, Created Date,...
                3: Người dùng chọn sort name lần nữa 
            */


            if (!keepSort)
            {
                //Trường hợp 2
                if (!currentSortByParam.Equals(sortByParam))
                {
                    sortOrderParam = "asc";
                }
                // Trường hợp 3
                else
                {
                    if (sortOrderParam == "asc") // asc => desc
                    {
                        sortOrderParam = "desc";
                    }
                    else //desc => bỏ sort
                    {
                        sortOrderParam = "";
                        sortByParam = "";
                    }
                }
            }

            sortBy = sortByParam;
            sortOrder = sortOrderParam;

            //Razor page ngu vl nên phải thêm cái này để so sánh 2 cái sort order cũ vs mới
            currentSortBy = sortByParam;

            var emailPagination = _emailRepo.GetList(statusesParam, categoriesParam, searchtermParam, sortByParam, sortOrderParam, pageNumberParam, pageSizeParam);
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
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (excelFile != null && excelFile.Length > 0)
                {
                    await _emailRepo.ImportEmailTemplates(excelFile, _userManager.GetUserId(User));
                    TempData["success"] = "Import email templates successfully";
                }
                else
                {
                    TempData["error"] = "File not found!";
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Redirect("/admin/email");
        }


        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OnGetExportExcel(string[] statusesParam, string[] categoriesParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            categories = categoriesParam;
            statuses = statusesParam;
            searchtearm = searchtermParam;
            try
            {
                var md = await _emailRepo.ExportEmailFilter(statusesParam, categoriesParam, searchtermParam, pageNumberParam, pageSizeParam);
                if (md != null)
                {
                    return File(md, "application/octet-stream", "EmailTemplate.xlsx");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();

            // //Reinit Email Template For Normal Display In Index
            // var emailPagination = _emailRepo.GetList(statusesParam, categoriesParam, searchtermParam, pageNumberParam, pageSizeParam);
            // emailTemplates = emailPagination.listEmail;
            // TotalPages = emailPagination.totalPages;

            // if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            // {
            //     return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, categories = categoriesParam });
            // }
            // return Page();
        }
    }
}
