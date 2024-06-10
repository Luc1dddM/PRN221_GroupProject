using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Pages.Cart
{
    public class CartModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartModel(Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<CartDetail> CartDetail { get; set; } = default!;

        [BindProperty]
        public CartDetail CartDetailPostModel { get; set; } = default!; //handle input from view

        public async Task OnGetAsync()
        {
            //get the cart details of specific user
            var userId = _userManager.GetUserId(User);
            var cartDetails = await _context.CartDetails
                .Where(cd => cd.UserId == userId)
                .AsNoTracking()
                .Include(cd => cd.Cart)
                .Include(cd => cd.Product)
                .ToListAsync();
            CartDetail = cartDetails;

            /*//filter any CartDetail that has been converted to OrderDetail
            var orderHeader = await _context.OrderHeaders //get the orderHeader of a specific user
                .Where(oh => oh.UserId == userId)
                .AsNoTracking()
                .Select(oh => oh.OrderHeaderId)
                .ToListAsync();

            var orderDetails = await _context.OrderDetails //get orderDetails of oderHeader
                .Where(od => orderHeader.Contains(od.OrderHeaderId))
                .Select(od => od.ProductId)
                .ToListAsync();
            //filter any CartDetail which convert to OrderDetail
            CartDetail = cartDetails.Where(cd => !orderDetails.Contains(cd.ProductId)).ToList();*/
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            try
            {
                var existingCart = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.CartDetailId == CartDetailPostModel.CartDetailId &&
                                                                                            cd.ProductId == CartDetailPostModel.ProductId);
                if (existingCart != null)
                {
                    existingCart.Count = CartDetailPostModel.Count;
                    _context.CartDetails.Update(existingCart);
                }
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync()
        {
            try
            {
                var productRemove = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.CartDetailId == CartDetailPostModel.CartDetailId);
                if (productRemove != null)
                {
                    _context.CartDetails.Remove(productRemove);
                }
                await _context.SaveChangesAsync();
                TempData["success"] = "Product has been deleted from cart.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }
    }
}
