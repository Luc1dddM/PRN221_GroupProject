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

namespace PRN221_GroupProject.Pages.CustomerProducts
{
    public class DetailsModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Product Product { get; set; } = default!;
        public CartHeader CartHeader { get; set; }

        [BindProperty]
        public CartDetail CartDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
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
