using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Enums;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository;
using PRN221_GroupProject.Repository.Carts;
using PRN221_GroupProject.Repository.Orders;
using PRN221_GroupProject.Repository.ProductCategories;

namespace PRN221_GroupProject.Pages.Cart
{
    [BindProperties]
    public class CheckoutModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailRepository _emailRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductCategorieRepository _productCategorieRepository;

        public CheckoutModel(Prn221GroupProjectContext context,
                             UserManager<ApplicationUser> userManager,
                             IEmailRepository emailRepository,
                             ICartRepository cartRepository,
                             IOrderRepository orderRepository,
                             IProductCategorieRepository productCategorieRepository)
        {
            _context = context;
            _userManager = userManager;
            _emailRepository = emailRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productCategorieRepository = productCategorieRepository;
        }

        public OrderHeader OrderHeader { get; set; } = default!;
        public OrderDetail OrderDetail { get; set; } = default!;
        public IList<CartDetail> CartDetail { get; set; } = default!;

        //variable calculate the total price
        public double totalPrice { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var cartDetails = _cartRepository.GetCartDetailsByUserId(userId);
            CartDetail = cartDetails;

            totalPrice = CartDetail.Sum(cd => cd.Product.Price * cd.Count);
        }


        public async Task<IActionResult> OnPost()
        {
            try
            {
                //get authorize user id
                var userId = _userManager.GetUserId(User);
                await _orderRepository.CreateOrderHeader(OrderHeader, userId);

                //get any user's CartDetail existed in cart to convert into OrderDetail
                var cartDetails = _cartRepository.GetCartDetailsByUserId(userId);

                //create new record of OrderDetail for each CartDetail
                foreach (var cartDetailItem in cartDetails)
                {
                    await _orderRepository.CreateOrderDetail(cartDetailItem, OrderHeader.OrderHeaderId);

                    //decrease the quantity of the product after each orderDetail is created
                    var productCategories = _context.ProductCategories.Include(pc => pc.Category)
                                                                      .FirstOrDefault(pc => pc.ProductId.Equals(cartDetailItem.ProductId) &&
                                                                                            pc.Category.Name.Equals(cartDetailItem.Color));
                    if (productCategories.Quantity >= cartDetailItem.Count)
                    {
                        productCategories.Quantity -= cartDetailItem.Count;
                        _context.SaveChanges();
                    }

                    //safely remove any CartDetail after convert into OrderDetail and decrease the quantity in the Product_Category
                    var cartDetailRemove = _cartRepository.GetCartDetailById(cartDetailItem.CartDetailId);
                    if (cartDetailRemove != null)
                    {
                        _cartRepository.DeleteCartDetail(cartDetailRemove, userId);
                    }
                }

                if (!string.IsNullOrEmpty(OrderHeader.Email))
                {
                    var order = _orderRepository.GetOrderHeaderById(OrderHeader.OrderHeaderId);
                    await _emailRepository.SendEmailOrder(order);
                }

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
