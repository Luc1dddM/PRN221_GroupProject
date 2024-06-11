using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
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
        public IndexModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
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
                var coupon = _context.Coupons.FirstOrDefault(); //may not need coupon in cart

                //check if cart header for user existed or create new
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
                if (CartHeader == null)
                {
                    CartHeader = new CartHeader
                    {
                        UserId = userId,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                    };
                    _context.CartHeaders.Add(CartHeader);
                    await _context.SaveChangesAsync();
                }

                //check if the product is already in the cart
                var existCardDetail = await _context.CartDetails.FirstOrDefaultAsync(c => c.CartId == CartHeader.CartId &&
                                                                                          c.ProductId == CartDetail.ProductId);
                //if product is already in cart, increase quantity
                if (existCardDetail != null)
                {
                    existCardDetail.Count += CartDetail.Count;
                }
                else
                {
                    //add new product to cart
                    CartDetail.CartId = CartHeader.CartId;
                    CartDetail.UserId = userId;
                    CartDetail.CreatedBy = userId;
                    CartDetail.CreatedDate = DateTime.Now;
                    _context.CartDetails.Add(CartDetail);
                }

                await _context.SaveChangesAsync();
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
