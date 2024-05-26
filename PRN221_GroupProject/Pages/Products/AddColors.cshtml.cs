using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.ProductCategories;
using PRN221_GroupProject.Repository.Products;

namespace PRN221_GroupProject.Pages.Products
{
    public class AddColorsModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;
        public IProductCategorieRepository _productCategorieRepository;

        public AddColorsModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, 
            ICategoryRepository categoryRepository, 
            IProductRepository productRepository,
            IProductCategorieRepository productCategorieRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productCategorieRepository = productCategorieRepository;
        }

        [BindProperty]
        public ProductCategory ProductCategory { get; set; } = default!;
        public List<Category> Colors { get; set; } = default!;

        public IActionResult OnGet(string? ProductId)
        {

            if (ProductId == null)
            {
                return NotFound();
            }
            Colors = _categoryRepository.GetColors(_productRepository.GetProductByIDInclude(ProductId));
            ViewData["ProductId"] = ProductId;
            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ProductCategory.CategoryId = Request.Form["categories"].ToString();
            _productCategorieRepository.CreateProductCategory(ProductCategory);

            return RedirectToPage("./Index");
        }
    }
}
