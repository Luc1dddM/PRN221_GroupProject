using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.ProductCategories;
using PRN221_GroupProject.Repository.Products;

namespace PRN221_GroupProject.Pages.Products
{
    public class EditProductCategoryModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;
        public IProductCategorieRepository _productCategorieRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public EditProductCategoryModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IProductCategorieRepository productCategorieRepository,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productCategorieRepository = productCategorieRepository;
            _userManager = userManager;
        }

        [BindProperty]
        public ProductCategory ProductCategory { get; set; } = default!;
        public List<ProductCategory> ProductCategories { get; set; } = default!;
        public List<Category> ChoosedCategories { get; set; } = default!;
        public List<Category> Brands { get; set; } = default!;
        public List<Category> Devices { get; set; } = default!;
        public bool hasBrand { get; set; }
        public bool hasDevice { get; set; }
        [BindProperty]
        public string brand { get; set; }
        [BindProperty]
        public string device { get; set; }




        public async Task<IActionResult> OnGetAsync(string ProductId, string categoryId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }
            if (categoryId != null)
            {
                _productCategorieRepository.DeleteProductCategory(ProductId, categoryId);
            }
            var product = _productRepository.GetProductByID(ProductId);
            ChoosedCategories = _categoryRepository.GetChoosedCategoriesByProduct(product);
            Brands = _categoryRepository.GetBrandsByProduct(product);
            Devices = _categoryRepository.GetDevicesByProduct(product);
            hasBrand = _categoryRepository.haveBrand(product);
            hasDevice = _categoryRepository.haveDevice(product);
            ViewData["ProductId"] = ProductId;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                
                _productCategorieRepository.CreateProductCategories(ProductCategory, brand, device, _userManager.GetUserId(User));
                TempData["success"] = "Edit product category successfully";

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }


            return RedirectToPage("./Index");
        }

    }
}
