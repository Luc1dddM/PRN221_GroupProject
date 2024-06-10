using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.Products;

namespace PRN221_GroupProject.Pages.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public IProductRepository _productRepository;
        public ICategoryRepository _categoryRepository;
        public IndexModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        [BindProperty]
        public string[] Brands { get; set; }
        public string[] Devices { get; set; }
        public string[] Colors { get; set; }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public string searchtearm { get; set; }
        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public IList<Product> Product { get; set; } = default!;
        public List<Category> Brand {  get; set; } = default!;
        public List<Category> Device { get; set; } = default!;
        public List<Category> Color { get; set; } = default!;




        public  IActionResult OnGet(string StartPrice, string EndPrice,string[] colorsParam, string[] brandsParam, string[] devicesParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            Product =  _productRepository.GetAll();
            Brand = _categoryRepository.GetBrands();
            Device = _categoryRepository.GetDevices();
            Color = _categoryRepository.GetColors();

            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            Devices = devicesParam;
            Brands = brandsParam;
            Colors = colorsParam;
            searchtearm = searchtermParam;
            Price1 = StartPrice;
            Price2 = EndPrice;

            var emailPagination = _productRepository.GetList(colorsParam,brandsParam, devicesParam, StartPrice, EndPrice, searchtermParam, pageNumberParam, pageSizeParam);

            Product = emailPagination.listProduct;
            TotalPages = emailPagination.totalPages;

            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, Brands = brandsParam,  Devices = devicesParam});
            }


            return Page();
            
        }
    }
}
