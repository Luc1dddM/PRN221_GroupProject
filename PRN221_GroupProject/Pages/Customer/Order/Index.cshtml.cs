using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Orders;

namespace PRN221_GroupProject.Pages.Customer.Order
{
    [Authorize(Policy = "customer")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;

        public IndexModel(IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        /*-----filter variable------*/
        public string[] statuses { get; set; }
        public string[] categories { get; set; }
        /*-------------------------*/

        /*-----search variable------*/
        public string searchtearm { get; set; }
        /*-------------------------*/

        /*-----pagination variable------*/
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        /*-------------------------*/

        /*-----sort variable------*/
        public string sortBy { get; set; }
        public string sortOrder { get; set; }
        public string currentSortBy { get; set; }
        /*-------------------------------------*/

        public IList<OrderHeader> OrderHeader { get; set; } = default!;


        //"Optional parameters must appear after all required parameters" will appear when put required field after the optional (Optional parameters are those that have default values provided)
        public IActionResult OnGet(string[] statusesParam, string[] categoriesParam,
                                        bool keepSort = false,
                                        string currentSortByParam = "", string sortByParam = "", string sortOrderParam = "",
                                        string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            //Set Params for current value of model
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            categories = categoriesParam;
            statuses = statusesParam;
            searchtearm = searchtermParam;
            var userIdParam = _userManager.GetUserId(User);
            //Change the Sort By and Sort Order for the next sort
            /*Đọc Kĩ Hướng Dẫn Sử Dụng Trước Khi Dùng!
            -Ý tưởng: Nếu sort hiện tại là sort theo name người dùng sort name lần nữa=> SortBy Sẽ thay đổi (asc => desc => no sort)
            -Mình sẽ thay đổi sortBy để truyền qua bên Index 
            Ví dụ: hiện tại biến SortBy đang là Name, SortOrder đang là asc => Lần đầu người dùng chọn sort by name
            Vậy sẽ có 3 trường hợp: 
                1: Giữ Nguyên sort (người dùng chuyển trang, filter, ...)
                2: Người dùng chọn sort bằng trường khác: category, Created Date,...
                3: Người dùng chọn sort name lần nữa 
            */

            if (!keepSort)
            {
                //Trường hợp 2
                if (!currentSortByParam.Equals(sortByParam))
                {
                    sortOrderParam = "asc";
                }
                // Trường hợp 3
                else
                {
                    if (sortOrderParam == "asc") // asc => desc
                    {
                        sortOrderParam = "desc";
                    }
                    else //default case or "desc" => bỏ sort
                    {
                        sortOrderParam = "";
                        sortByParam = "";
                    }
                }
            }

            sortBy = sortByParam;
            sortOrder = sortOrderParam;

            //Save to current sort to compare new sort order with old one 
            currentSortBy = sortByParam;

            var customerOrderPagination = _orderRepository.GetCustomerList(statusesParam, categoriesParam, sortByParam, sortOrderParam, searchtermParam, pageNumberParam, pageSizeParam, userIdParam);
            OrderHeader = customerOrderPagination.listOrder;
            TotalPages = customerOrderPagination.totalPages;

            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, categories = categoriesParam });
            }

            return Page();
        }
    }
}
