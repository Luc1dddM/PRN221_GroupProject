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
    public class IndexModel : PageModel
    {
        public IOrderRepository _orderRepository;

        public IndexModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public string[] statuses { get; set; }
        public string[] categories { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public string searchtearm { get; set; }


        public IList<OrderHeader> OrderHeader { get;set; } = default!;

        public IActionResult OnGetAsync(string[] statusesParam, string[] categoriesParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            categories = categoriesParam;
            statuses = statusesParam;
            searchtearm = searchtermParam;

            var orderPagination = _orderRepository.GetList(statusesParam, categoriesParam, searchtermParam, pageNumberParam, pageSizeParam);
            OrderHeader = orderPagination.listOrder;
            TotalPages = orderPagination.totalPages;

            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, categories = categoriesParam });
            }

            return Page();
        }
    }
}
