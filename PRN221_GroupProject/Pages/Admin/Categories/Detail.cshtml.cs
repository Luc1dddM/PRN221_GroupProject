using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.ProductCategories;
using PRN221_GroupProject.Repository.Products;

namespace PRN221_GroupProject.Pages.Categories
{
    public class DetailModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public ICategoryRepository _categoryRepository;
        public IProductCategorieRepository _productCategoryRepository;
        public IProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public DetailModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, 
            ICategoryRepository categoryRepository, 
            IProductCategorieRepository productCategorieRepository, 
            IProductRepository productRepository, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategorieRepository;
            _productRepository = productRepository;
            _userManager = userManager;
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

            var userId = _userManager.GetUserId(User);
            try
            {
                
                if(Category.Status)
                {
                    if (!Category.Type.Equals("Color"))
                    {
                        foreach (var Productcategory in _productCategoryRepository.GetProductCategoriesByCategoryID(Category.CategoryId))
                        {
                            _productCategoryRepository.EnableByProduct(Productcategory.ProductId, userId);
                            _productRepository.Enable(Productcategory.ProductId, userId);
                        }
                    }
                    _productCategoryRepository.EnableByCategory(Category.CategoryId, userId);
                }
                else
                {
                    if (!Category.Type.Equals("Color"))
                    {
                        foreach (var Productcategory in _productCategoryRepository.GetProductCategoriesByCategoryID(Category.CategoryId))
                        {
                            _productCategoryRepository.DisableByProduct(Productcategory.ProductId, userId);
                            _productRepository.Disable(Productcategory.ProductId, userId);
                        }
                    }
                    _productCategoryRepository.DisableByCategory(Category.CategoryId, userId);
                }
                Category.UpdatedBy = _userManager.GetUserId(User);
                _categoryRepository.update(Category, userId);
                TempData["success"] = "Update Category successfully";

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToPage("./Index");
        }


    }
}
