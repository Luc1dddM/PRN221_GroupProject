using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Pages.Cart
{
    public class CartModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        public CartModel(Prn221GroupProjectContext context)
        {
            _context = context;
        }

        public IList<CartDetail> CartDetail { get; set; } = default!;

        [BindProperty]
        public CartDetail CartDetailPostModel { get; set; } = default!; //handle input from view

        public async Task OnGetAsync() //currently Get all cartdetail, since there is no user identifier 
        {
            CartDetail = await _context.CartDetails.AsNoTracking()
                .Include(cd => cd.Cart)
                .Include(cd => cd.Product)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {

            var existingCart = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.CartDetail1 == CartDetailPostModel.CartDetail1 &&
                                                                            cd.ProductId == CartDetailPostModel.ProductId);
            if (existingCart != null)
            {
                existingCart.Count = CartDetailPostModel.Count;
                _context.CartDetails.Update(existingCart);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveAsync()
        {
            var productRemove = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.CartDetail1 == CartDetailPostModel.CartDetail1);
            if (productRemove != null)
            {
                _context.CartDetails.Remove(productRemove);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
