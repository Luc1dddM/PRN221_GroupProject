using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
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

        public async Task<IActionResult> OnGetDownloadTemplateAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "excelTemplates", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public async Task<IActionResult> OnPostUploadExcel(IFormFile excelFile)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (excelFile != null && excelFile.Length > 0)
                {
                    await _productRepository.ImportProducts(excelFile, _userManager.GetUserId(User));
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
            return Redirect("/admin/Products");
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OnGetExportExcel(string StartPrice, string EndPrice, string[] colorsParam, string[] brandsParam, string[] devicesParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            Devices = devicesParam;
            Brands = brandsParam;
            Colors = colorsParam;
            searchtearm = searchtermParam;
            Price1 = StartPrice;
            Price2 = EndPrice;
            try
            {
                var md = await _productRepository.ExportProductsFilter(colorsParam, brandsParam, devicesParam, StartPrice, EndPrice, searchtermParam, pageNumberParam, pageSizeParam);
                if (md != null)
                {
                    return File(md, "application/octet-stream", "Products.xlsx");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            Product = _productRepository.GetAll();
            Brand = _categoryRepository.GetBrands();
            Device = _categoryRepository.GetDevices();
            Color = _categoryRepository.GetColors();
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
