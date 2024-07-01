using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Carts;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.Products;

namespace PRN221_GroupProject.Pages.CustomerProducts
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Prn221GroupProjectContext _context;

        public IProductRepository _productRepository;
        public ICategoryRepository _categoryRepository;
        public ICartRepository _cartRepository;
        public IndexModel(Prn221GroupProjectContext context,
                          IProductRepository productRepository,
                          ICategoryRepository categoryRepository,
                          UserManager<ApplicationUser> userManager,
                          ICartRepository cartRepository)
        {
            _userManager = userManager;
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _cartRepository = cartRepository;
        }

        [BindProperty]
        public string[] Brands { get; set; }

        [BindProperty]
        public IList<Product> Product { get; set; } = default!;
        public string[] Devices { get; set; }
        public string[] Colors { get; set; }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public string searchtearm { get; set; }
        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public List<Category> Brand { get; set; } = default!;
        public List<Category> Device { get; set; } = default!;
        public List<Category> Color { get; set; } = default!;

        public CartHeader CartHeader { get; set; } = default!;
        [BindProperty]
        public CartDetail CartDetail { get; set; } = default!;

        //this attribute is for get the color of one product
        public Dictionary<string, string> ProductColors { get; set; } = new Dictionary<string, string>();

        public IActionResult OnGetAsync(string StartPrice, string EndPrice, string[] colorsParam, string[] brandsParam, string[] devicesParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            Product = _productRepository.GetAll();
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

            var emailPagination = _productRepository.GetListCustomer(colorsParam, brandsParam, devicesParam, StartPrice, EndPrice, searchtermParam, pageNumberParam, pageSizeParam);

            Product = emailPagination.listProduct;
            TotalPages = emailPagination.totalPages;

            foreach (var product in Product)
            {
                var categoryColors = _context.Categories.Include(ct => ct.ProductCategories)
                                                        .ThenInclude(pc => pc.Product)
                                                        .Where(ct => ct.Type.Equals("Color") &&
                                                                     ct.ProductCategories.Any(pc => pc.ProductId.Equals(product.ProductId)))
                                                        .Select(ct => ct.Name)
                                                        .FirstOrDefault();

                ProductColors[product.ProductId] = categoryColors;
            }

            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, Brands = brandsParam, Devices = devicesParam });
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                string userId = _userManager.GetUserId(User);
                _cartRepository.CreateCartHeader(userId);
                var cartHeader = _cartRepository.GetCartHeaderByUserId(userId);
                var cartDetail = _cartRepository.GetCartDetailByCartId_ProId(cartHeader.CartId, CartDetail.ProductId, CartDetail.Color);
                if (cartDetail != null)
                {
                    _cartRepository.UpdateCartDetailQuantity(CartDetail, userId);
                }
                else
                {
                    _cartRepository.CreateCartDetail(CartDetail, userId);
                }
                TempData["success"] = $"Product has been added to your cart.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }
    }
}
