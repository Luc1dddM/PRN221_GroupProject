using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Carts;

namespace PRN221_GroupProject.Pages.CustomerProducts
{
    public class DetailsModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ICartRepository _cartRepository;

        public DetailsModel(Prn221GroupProjectContext context, 
                            UserManager<ApplicationUser> userManager,
                            ICartRepository cartRepository)
        {
            _context = context;
            _userManager = userManager;
            _cartRepository = cartRepository;
        }

        public Product Product { get; set; } = default!;

        [BindProperty]
        public CartDetail CartDetail { get; set; } = default!;

        public string[] ProductColors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            var categoryColor = _context.Categories.Include(ct => ct.ProductCategories)
                                                           .ThenInclude(pc => pc.Product)
                                                           .Where(ct => ct.Type.Equals("Color") &&
                                                                        ct.ProductCategories.Any(pc => pc.ProductId.Equals(product.ProductId)))
                                                           .Select(ct => ct.Name).ToArray();
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
                ProductColors = categoryColor;
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
