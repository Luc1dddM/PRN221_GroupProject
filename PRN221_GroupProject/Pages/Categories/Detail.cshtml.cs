using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.ProductCategories;
using PRN221_GroupProject.Repository.Products;
using Syncfusion.EJ2.Linq;

namespace PRN221_GroupProject.Pages.Categories
{
    public class DetailModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public ICategoryRepository _categoryRepository;
        public IProductCategorieRepository _productCategoryRepository;
        public IProductRepository _productRepository;

        public DetailModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, ICategoryRepository categoryRepository, IProductCategorieRepository productCategorieRepository, IProductRepository productRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategorieRepository;
            _productRepository = productRepository;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;
        public IList<ProductCategory> ProductCategories { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(string? Categoryid)
        {
            if (Categoryid == null)
            {
                return NotFound();
            }

            

            var category =  _categoryRepository.GetCategoryByID(Categoryid);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {


            try
            {
                
                if(Category.Status)
                {
                    if (!Category.Type.Equals("Color"))
                    {
                        foreach (var Productcategory in _productCategoryRepository.GetProductCategoriesByCategoryID(Category.CategoryId))
                        {
                            _productCategoryRepository.EnableByProduct(Productcategory.ProductId);
                            _productRepository.Enable(Productcategory.ProductId);
                        }
                    }
                    _productCategoryRepository.EnableByCategory(Category.CategoryId);
                }
                else
                {
                    if (!Category.Type.Equals("Color"))
                    {
                        foreach (var Productcategory in _productCategoryRepository.GetProductCategoriesByCategoryID(Category.CategoryId))
                        {
                            _productCategoryRepository.DisableByProduct(Productcategory.ProductId);
                            _productRepository.Disable(Productcategory.ProductId);
                        }
                    }
                    _productCategoryRepository.DisableByCategory(Category.CategoryId);
                }

                _categoryRepository.update(Category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToPage("./Index");
        }


    }
}
