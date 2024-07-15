using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Irony.Parsing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Policy = "admin")]
    public class EditColorsModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;
        public IProductCategorieRepository _productCategorieRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public EditColorsModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context,
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
            try
            {
                if (ProductCategory.CategoryId == null || ProductCategory.Quantity == null)
                {
                    if (ProductCategory.CategoryId == null)
                    {
                        ModelState.AddModelError("ProductCategory.CategoryId", "The field color can not be null!");
                    }
                    if (ProductCategory.Quantity == null)
                    {
                        ModelState.AddModelError("ProductCategory.Quantity", "The field quantity can not be null!");
                    }
                    Colors = _categoryRepository.GetChoosedColors(_productRepository.GetProductByIDInclude(ProductCategory.ProductId));
                    var ProductCategories = _productCategorieRepository.GetProductCategoriesByProductID(ProductCategory.ProductId);
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    ViewData["ProductCategories"] = JsonConvert.SerializeObject(ProductCategories, settings);
                    ViewData["ProductId"] = ProductCategory.ProductId;
                    return Page();
                }
                if (ProductCategory.Quantity < 0)
                {

                    if (ProductCategory.Quantity < 0)
                    {
                        ModelState.AddModelError("ProductCategory.Quantity", "The quantity must be greater than or equal to 0!");
                    }
                    Colors = _categoryRepository.GetChoosedColors(_productRepository.GetProductByIDInclude(ProductCategory.ProductId));
                    var ProductCategories = _productCategorieRepository.GetProductCategoriesByProductID(ProductCategory.ProductId);
                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    ViewData["ProductCategories"] = JsonConvert.SerializeObject(ProductCategories, settings);
                    ViewData["ProductId"] = ProductCategory.ProductId;
                    return Page();
                }

                ProductCategory.CategoryId = Request.Form["categories"].ToString();
                _productCategorieRepository.UpdateProductCategories(ProductCategory, _userManager.GetUserId(User));
                TempData["success"] = "Edit color successfully";

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToPage("./Index");
        }

    }
}
