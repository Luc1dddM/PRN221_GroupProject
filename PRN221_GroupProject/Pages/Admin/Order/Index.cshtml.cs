using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Pages.Admin.Order
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<OrderHeader> OrderHeader { get;set; } = default!;

        public async Task OnGetAsync()
        {
            OrderHeader = await _context.OrderHeaders
                .Include(o => o.Coupon)
                .Include(o => o.User).ToListAsync();
        }
    }
}
