using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Orders;

namespace PRN221_GroupProject.Pages.Admin.Order
{
    [Authorize(Policy = "admin")]
    [BindProperties]
    public class DetailsModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;

        public DetailsModel(Prn221GroupProjectContext context,
                            UserManager<ApplicationUser> userManager,
                            IOrderRepository orderRepository)
        {
            _context = context;
            _userManager = userManager;
            _orderRepository = orderRepository;
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

            var orderDetails = await _context.OrderDetails //get orderDetails of orderHeader
                .Where(od => od.OrderHeaderId == orderHeaderId)
                .AsNoTracking()
                .ToListAsync();
            OrderDetails = orderDetails;

            var productIds = OrderDetails.Select(od => od.ProductId).ToList();
            Products = await _context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync();

            totalPrice = OrderDetails.Sum(od => od.Price * od.Count);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var orderHeader = _orderRepository.GetOrderHeaderById(OrderHeader.OrderHeaderId);
                _orderRepository.AdminChangeOrderStatus(orderHeader, userId);

                TempData["success"] = $"Order status has been updated.";
                return RedirectToPage("./Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
