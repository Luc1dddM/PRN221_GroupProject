using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IndexModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        public string[] Type { get; set; }
        public string[] statuses { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public string searchtearm { get; set; }

        public IList<Category> Category { get;set; } = default!;

        public IActionResult OnGet(string[] statusesParam, string[] TypeParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            Type = TypeParam;
            statuses = statusesParam;
            searchtearm = searchtermParam;

            var emailPagination = _categoryRepository.GetList(statusesParam, TypeParam, searchtermParam, pageNumberParam, pageSizeParam);

            Category = emailPagination.listCategory;
            TotalPages = emailPagination.totalPages;

            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, categories = TypeParam });
            }


            return Page();

        }
    }
}
