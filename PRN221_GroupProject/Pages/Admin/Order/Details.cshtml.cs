using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Pages.Admin.Order
{
    [BindProperties]
    public class DetailsModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;

        public DetailsModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context)
        {
            _context = context;
        }

        public OrderHeader OrderHeader { get; set; } = default!;
        public IList<OrderDetail> OrderDetails { get; set; } = default!;

        public IList<Product> Products { get; set; } = default!;

        public double totalPrice { get; set; }

        public async Task<IActionResult> OnGetAsync(string? orderHeaderId)
        {
            if (orderHeaderId == null)
            {
                return NotFound();
            }

            var orderheader = await _context.OrderHeaders.FirstOrDefaultAsync(m => m.OrderHeaderId == orderHeaderId);
            if (orderheader == null)
            {
                return NotFound();
            }
            else
            {
                OrderHeader = orderheader;
            }

            var orderDetails = await _context.OrderDetails //get orderDetails of oderHeader
                .Where(od => od.OrderHeaderId == orderHeaderId)
                .AsNoTracking()
                .ToListAsync();
            OrderDetails = orderDetails;

            var productIds = OrderDetails.Select(od => od.ProductId).ToList();
            Products = await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            totalPrice = OrderDetails.Sum(od => od.Price * od.Count);

            return Page();
        }

       /* public async Task<IActionResult> OnPostAsync()
        {

        }*/
    }
}
