using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Carts;

namespace PRN221_GroupProject.Pages.Cart
{
    public class CartModel : PageModel
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ICartRepository _cartRepository;

        public CartModel(Prn221GroupProjectContext context,
                         UserManager<ApplicationUser> userManager,
                         ICartRepository cartRepository)
        {
            _context = context;
            _userManager = userManager;
            _cartRepository = cartRepository;
        }

        public IList<CartDetail> CartDetail { get; set; } = default!;
        public Product Product { get; set; } = default!;

        public Dictionary<string, string[]> ProductColors { get; set; } = new Dictionary<string, string[]>(); // Dictionary to hold colors for each product

        [BindProperty]
        public CartDetail CartDetailPostModel { get; set; } = default!; //handle input from view

        public async Task OnGetAsync()
        {
            //get the cart details of specific user
            var userId = _userManager.GetUserId(User);
            var cartDetails = _cartRepository.GetCartDetailsByUserId(userId);
            CartDetail = cartDetails;
            //get product color base on ProductId of CartDetail
            foreach (var product in CartDetail)
            {
                var categoryColor = _context.Categories.Include(ct => ct.ProductCategories)
                                                                           .ThenInclude(pc => pc.Product)
                                                                           .ThenInclude(p => p.CartDetails)
                                                                           .Where(ct => ct.Type.Equals("Color") &&
                                                                                        ct.ProductCategories.Any(pc => pc.ProductId.Equals(product.ProductId)))
                                                                           .Select(ct => ct.Name).ToArray();
                ProductColors[product.ProductId] = categoryColor;
            }
        }

        public async Task<IActionResult> OnPostUpdateQuantityAsync()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var cartHeader = _cartRepository.GetCartHeaderByUserId(userId);
                var cartDetailUpdate = _cartRepository.GetCartDetailByCartId_ProId(cartHeader.CartId, CartDetailPostModel.ProductId, CartDetailPostModel.Color);
                if (cartDetailUpdate != null)
                {
                    _cartRepository.UpdateCartDetailQuantity(CartDetailPostModel, userId);
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateQuantityInputFormAsync()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var cartHeader = _cartRepository.GetCartHeaderByUserId(userId);
                var cartDetailUpdate = _cartRepository.GetCartDetailByCartId_ProId(cartHeader.CartId, CartDetailPostModel.ProductId, CartDetailPostModel.Color);
                if (cartDetailUpdate != null)
                {
                    _cartRepository.UpdateCartDetailQuantityByInputField(CartDetailPostModel, userId);
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }

        /*public async Task<IActionResult> OnPostUpdateColorAsync()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var cartHeader = _cartRepository.GetCartHeaderByUserId(userId);
                var cartDetailUpdate = _cartRepository.GetCartDetailByCartId_ProId(cartHeader.CartId, CartDetailPostModel.ProductId, CartDetailPostModel.Color);
                if (cartDetailUpdate != null)
                {
                    _cartRepository.UpdateCartDetailColor(CartDetailPostModel, userId);
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }*/

        public async Task<IActionResult> OnPostRemoveAsync()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var cartHeader = _cartRepository.GetCartHeaderByUserId(userId);
                var cartDetailRemove = _cartRepository.GetCartDetailByCartId_ProId(cartHeader.CartId, CartDetailPostModel.ProductId, CartDetailPostModel.Color);
                if (cartDetailRemove != null)
                {
                    _cartRepository.DeleteCartDetail(cartDetailRemove, userId);
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();
        }
    }
}
