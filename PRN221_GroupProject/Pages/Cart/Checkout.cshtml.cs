using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Enum;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository;

namespace PRN221_GroupProject.Pages.Cart
{
    [BindProperties]
    public class CheckoutModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailRepository _emailRepository;

        public CheckoutModel(Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager, IEmailRepository emailRepository)
        {
            _context = context;
            _userManager = userManager;
            _emailRepository = emailRepository;
        }

        public OrderHeader OrderHeader { get; set; } = default!;
        public OrderDetail OrderDetail { get; set; } = default!;
        public IList<CartDetail> CartDetail { get; set; } = default!;

        //variable calculate the total price
        public double totalPrice { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var cartDetails = await _context.CartDetails
                .Where(cd => cd.UserId == userId)
                .AsNoTracking()
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
                .AsNoTracking()
                .Select(od => od.ProductId)
                .ToListAsync();
            //filter any CartDetail which convert to OrderDetail
            CartDetail = cartDetails.Where(cd => !orderDetails.Contains(cd.ProductId)).ToList();*/

            totalPrice = CartDetail.Sum(cd => cd.Product.Price * cd.Count);
        }


        public async Task<IActionResult> OnPost()
        {
            try
            {
                /*  // Define valid payment methods
                  var validPaymentMethods = new[] { "Cash Delivery", "Online Banking" };

                  // Check if the provided payment method is valid
                  if (!validPaymentMethods.Contains(OrderHeader.PaymentMethod))
                  {
                      // Handle invalid payment method
                      TempData["error"] = "Invalid payment method.";
                      return Page();
                  }*/

                //get authorize user id
                var userId = _userManager.GetUserId(User);

                //pass the value to field not in form
                OrderHeader.OrderStatus = OrderStatusEnum.Pending.ToString();
                OrderHeader.UserId = userId;
                OrderHeader.CreatedDate = DateTime.Now;
                OrderHeader.CreatedBy = userId;
                OrderHeader.UpdatedBy = userId;
                OrderHeader.UpdatedDate = DateTime.Now;

                _context.OrderHeaders.Add(OrderHeader);
                await _context.SaveChangesAsync();

                //get any user's CartDetail existed in cart to convert into OrderDetail
                var cartDetails = await _context.CartDetails
                 .Where(cd => cd.UserId == userId)
                 .AsNoTracking()
                 .Include(cd => cd.Product)
                 .ToListAsync();

                /*//filter any CartDetail that has been converted to OrderDetail
                var orderHeader = await _context.OrderHeaders //get the orderHeader of a specific user
                    .Where(oh => oh.UserId == userId)
                    .AsNoTracking()
                    .Select(oh => oh.OrderHeaderId)
                    .ToListAsync();

                var orderDetails = await _context.OrderDetails //get orderDetails of oderHeader
                    .Where(od => orderHeader.Contains(od.OrderHeaderId))
                    .AsNoTracking()
                    .Select(od => od.ProductId)
                    .ToListAsync();
                //filter any CartDetail which convert to OrderDetail
                CartDetail = cartDetails.Where(cd => !orderDetails.Contains(cd.ProductId)).ToList();*/

                //create new record of OrderDetail for each CartDetail
                foreach (var cartDetailItem in cartDetails)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderHeaderId = OrderHeader.OrderHeaderId,
                        ProductId = cartDetailItem.ProductId,
                        Count = cartDetailItem.Count,
                        Price = cartDetailItem.Product.Price,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                    };
                    _context.OrderDetails.Add(orderDetail);
                    //remove any CartDetail after convert into OrderDetail
                    var cartDetailRemove = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.CartDetailId == cartDetailItem.CartDetailId);
                    if (cartDetailRemove != null)
                    {
                        _context.CartDetails.Remove(cartDetailRemove);
                    }
                }
                await _context.SaveChangesAsync();


                /*await _emailRepository.SendEmailOrder(OrderHeader);*/


                return RedirectToPage("./OrderConfirmation", new { orderHeaderId = OrderHeader.OrderHeaderId });
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }
    }
}
