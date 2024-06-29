using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;

namespace PRN221_GroupProject.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public IndexModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        public string[] Type { get; set; }
        public string[] statuses { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public string searchtearm { get; set; }
        public string sortBy { get; set; }
        public string sortOrder { get; set; }
        public string currentSortBy { get; set; }

        public IList<Category> Category { get; set; } = default!;

        public IActionResult OnGet(string[] statusesParam, string[] TypeParam, bool keepSort = false, string currentSortByParam = "", string searchtermParam = "", string sortByParam = "", string sortOrderParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            //Set Params for current value of model
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            Type = TypeParam;
            statuses = statusesParam;
            searchtearm = searchtermParam;



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

            var categoriesPagination = _categoryRepository.GetList(statusesParam, TypeParam, searchtermParam, sortByParam, sortOrderParam, pageNumberParam, pageSizeParam);

            Category = categoriesPagination.listCategory;
            TotalPages = categoriesPagination.totalPages;

            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, categories = TypeParam });
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
                    await _categoryRepository.ImportCategories(excelFile, _userManager.GetUserId(User));
                    TempData["success"] = "Import Category list successfully";
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
            return Redirect("/admin/Categories");
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OnGetExportExcel(string[] statusesParam, string[] TypeParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            Type = TypeParam;
            statuses = statusesParam;
            searchtearm = searchtermParam;
            try
            {
                var md = await _categoryRepository.ExportCategoriesFilter(statusesParam, TypeParam, searchtermParam, pageNumberParam, pageSizeParam);
                if (md != null)
                {
                    return File(md, "application/octet-stream", "Category.xlsx");
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
