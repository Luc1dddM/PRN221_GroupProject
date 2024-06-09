using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Pages.Cart
{
    [BindProperties]
    public class OrderConfirmationModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderConfirmationModel(Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public OrderHeader OrderHeader { get; set; } = default!;
        public async Task<IActionResult> OnGet(string OrderHeaderId)
        {
            var userId = _userManager.GetUserId(User);
            OrderHeader = await _context.OrderHeaders
                .Where(oh => oh.UserId == userId && oh.OrderHeaderId == OrderHeaderId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (OrderHeader == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
