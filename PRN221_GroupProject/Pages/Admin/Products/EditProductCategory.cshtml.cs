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
        public List<Category> Categories { get; set; } = default!;




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
            ChoosedCategories = _categoryRepository.GetChoosedCategoriesByProduct(_productRepository.GetProductByID(ProductId));
            Categories = _categoryRepository.GetCategoriesByProduct(_productRepository.GetProductByID(ProductId));
            ViewData["ProductId"] = ProductId;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var categories = Request.Form["categories"].ToList();
                _productCategorieRepository.CreateProductCategories(ProductCategory, categories, _userManager.GetUserId(User));
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
