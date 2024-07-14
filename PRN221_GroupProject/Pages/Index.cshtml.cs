using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Orders;
using PRN221_GroupProject.Repository.Products;
using PRN221_GroupProject.Repository.UserMessages;

namespace PRN221_GroupProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public IProductRepository _ProductRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserMessagesRepository _userMessagesRepository;

        public IndexModel(ILogger<IndexModel> logger, Models.Prn221GroupProjectContext context, IProductRepository productRepository, IUserMessagesRepository userMessagesRepository,IOrderRepository orderRepository)
        {
            _logger = logger;
            _context = context;
            _ProductRepository = productRepository;
            _userMessagesRepository = userMessagesRepository;
            _orderRepository = orderRepository;
        }
        public IList<TopProductDTO> Product { get; set; } = default!;
        public void OnGet()
        {
            
            Product = _orderRepository.GetTop12Sales();

        }

        public IActionResult OnGetMessageNotification(string receiver)
        {
            try
            {
                return new JsonResult(_userMessagesRepository.CountMessagesUnReadAdmin(receiver));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}