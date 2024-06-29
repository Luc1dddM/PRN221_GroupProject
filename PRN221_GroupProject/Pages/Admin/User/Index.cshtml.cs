using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Users;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_GroupProject.Pages.User
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public IndexModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IList<UserListDTO> Users { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string SearchTerm { get; set; }

        public string[] statuses { get; set; }
        /*public string[] roles { get; set; }*/

        public async Task<IActionResult> OnGetAsync(string[] statusesParam, string[] rolesParam, string searchTermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
                PageSize = pageSizeParam;
                PageNumber = pageNumberParam;
                SearchTerm = searchTermParam;

                statuses = statusesParam;
                /*roles = rolesParam;*/

                var result = await _userRepository.GetUsers(statusesParam, rolesParam, searchTermParam, pageNumberParam, pageSizeParam);
                Users = result.Users;
                TotalPages = result.totalPages;

                if (PageNumber < 1 || (PageNumber > TotalPages && TotalPages > 0))
                {
                    return RedirectToPage(new { PageNumber = 1, PageSize = PageSize });
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
                    await _userRepository.ImportUsers(excelFile);
                    TempData["success"] = "Import user templates successfully";
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
            return Redirect("/admin/user");
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OnGetExportExcel(string[] statusesParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            PageSize = pageSizeParam;
            PageNumber = pageNumberParam;
            statuses = statusesParam;
            SearchTerm = searchtermParam;
            try
            {
                var md = await _userRepository.ExportUsers(statusesParam, searchtermParam, pageNumberParam, pageSizeParam);
                if (md != null)
                {
                    return File(md, "application/octet-stream", "UserTemplate.xlsx");
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
