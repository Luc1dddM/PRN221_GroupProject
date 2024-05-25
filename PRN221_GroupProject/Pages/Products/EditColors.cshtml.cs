using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.ProductCategories;
using PRN221_GroupProject.Repository.Products;

namespace PRN221_GroupProject.Pages.Products
{
    public class EditColorsModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;
        public IProductCategorieRepository _productCategorieRepository;

        public EditColorsModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context,
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

        public async Task<IActionResult> OnGetAsync(string ProductId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }
            try
            {
                Colors = _categoryRepository.GetChoosedColors(_productRepository.GetProductByIDInclude(ProductId));
                var ProductCategories = _productCategorieRepository.GetProductCategoriesByProductID(ProductId);

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                ViewData["ProductCategories"] = JsonConvert.SerializeObject(ProductCategories, settings);
                ViewData["ProductId"] = ProductId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            ProductCategory.CategoryId = Request.Form["categories"].ToString();
            _productCategorieRepository.UpdateProductCategories(ProductCategory);
            return RedirectToPage("./Index");
        }

    }
}
