using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Enums;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository;
using PRN221_GroupProject.Repository.Carts;
using PRN221_GroupProject.Repository.Coupons;
using PRN221_GroupProject.Repository.Orders;
using PRN221_GroupProject.Repository.ProductCategories;

namespace PRN221_GroupProject.Pages.Cart
{
    [Authorize(Policy = "customer")]
    [BindProperties]
    public class CheckoutModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailRepository _emailRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductCategorieRepository _productCategorieRepository;
        private readonly ICouponRepository _couponRepository;

        public CheckoutModel(Prn221GroupProjectContext context,
                             UserManager<ApplicationUser> userManager,
                             IEmailRepository emailRepository,
                             ICartRepository cartRepository,
                             IOrderRepository orderRepository,
                             IProductCategorieRepository productCategorieRepository,
                             ICouponRepository couponRepository)
        {
            _context = context;
            _userManager = userManager;
            _emailRepository = emailRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productCategorieRepository = productCategorieRepository;
            _couponRepository = couponRepository;
        }

        public OrderHeader OrderHeader { get; set; } = default!;
        public OrderDetail OrderDetail { get; set; } = default!;
        public IList<CartDetail> CartDetail { get; set; } = default!;

        [BindProperty]
        public string CouponCode { get; set; } = string.Empty;
        public double totalPrice { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var cartDetails = _cartRepository.GetCartDetailsByUserId(userId);
            CartDetail = cartDetails;

            if (TempData.ContainsKey("totalPrice"))
            {
                var totalPriceString = TempData["totalPrice"] as string;
                if (double.TryParse(totalPriceString, out double parsedTotalPrice))
                {
                    totalPrice = parsedTotalPrice;
                }
                else
                {
                    totalPrice = 0;
                }
            }
            else
            {
                totalPrice = CartDetail.Sum(cd => cd.Product.Price * cd.Count);
            }
            this.totalPrice = totalPrice;

        }

        public async Task<IActionResult> OnPostApplyCoupon(double totalPrice)
        {
            if (!string.IsNullOrEmpty(CouponCode))
            {
                var coupon = _couponRepository.GetCouponByCode(CouponCode);

                if (coupon != null && coupon.Status)
                {
                    if (totalPrice >= coupon.MinAmount && totalPrice <= coupon.MaxAmount)
                    {
                        totalPrice -= (totalPrice * (coupon.DiscountAmount / 100));
                        TempData["success"] = "Coupon applied successfully.";
                        TempData["totalPrice"] = totalPrice.ToString();
                        TempData["couponCode"] = CouponCode;

                    }
                    else
                    {
                        TempData["error"] = "Coupon does not meet the conditions.";
                        CouponCode = null;
                    }
                }
                else
                {
                    TempData["error"] = "Coupon is not valid.";
                    CouponCode = null;
                }
            } else
            {
                TempData["error"] = "Need to input Coupon Code";
                CouponCode = null;
            }
            return RedirectToPage("/Cart/Checkout", new { couponCode = CouponCode });
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
            return RedirectToPage("/cart/checkout");
        }

       
    }
}
