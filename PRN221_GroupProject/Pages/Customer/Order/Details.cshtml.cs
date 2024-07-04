using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Orders;

namespace PRN221_GroupProject.Pages.Customer.Order
{
    [Authorize(Policy = "customer")]
    public class DetailsModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;

        public DetailsModel(Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager, IOrderRepository orderRepository)
        {
            _context = context;
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public OrderHeader OrderHeader { get; set; } = default!;
        public IList<OrderDetail> OrderDetails { get; set; } = default!;
        public IList<Product> Products { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(string? OrderHeaderId)
        {
            if (OrderHeaderId == null)
            {
                return NotFound();
            }

            var orderHeader = _orderRepository.GetOrderHeaderById(OrderHeaderId);
            if (orderHeader == null)
            {
                return NotFound();
            }
            else
            {
                OrderHeader = orderHeader;
            }

            var productIds = orderHeader.OrderDetails.Select(od => od.ProductId).ToList();
            if (productIds == null)
            {
                return NotFound();
            }
            else
            {
                Products = await _context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostChangeStatusAsync(string? orderHeaderId)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                _orderRepository.CustomerChangeOrderStatus(orderHeaderId, userId);



                TempData["success"] = $"Order status has been updated.";
                return RedirectToPage("./Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IActionResult> OnPostCancelOrderAsync(string? orderHeaderId)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                _orderRepository.CancelOrderStatus(orderHeaderId, userId);

                TempData["success"] = $"Order has been cancelled.";
                return RedirectToPage("./Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
