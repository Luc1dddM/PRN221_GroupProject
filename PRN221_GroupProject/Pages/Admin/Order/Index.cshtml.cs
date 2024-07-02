using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Orders;
using System.Text;

namespace PRN221_GroupProject.Pages.Admin.Order
{
    [Authorize(Policy = "admin")]
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


        public IActionResult OnGetAsync(string[] statusesParam, string[] categoriesParam, 
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

            var orderPagination = _orderRepository.GetList(statusesParam, categoriesParam, sortByParam, sortOrderParam, searchtermParam, pageNumberParam, pageSizeParam);
            OrderHeader = orderPagination.listOrder;
            TotalPages = orderPagination.totalPages;

            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, categories = categoriesParam });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostImportExcel(IFormFile excelFile)
        {
            try
            {
                var user = _userManager.GetUserId(User);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (excelFile != null && excelFile.Length > 0)
                {
                    await _orderRepository.ImportOrdersFile(excelFile, user);
                    TempData["success"] = "Import Order list successfully";
                }
                else
                {
                    TempData["error"] = "File not found!";
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Redirect("/Admin/Order");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetExportExcel(string[] statusesParam, string[] categoriesParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            categories = categoriesParam;
            statuses = statusesParam;
            searchtearm = searchtermParam;
            try
            {
                var md = await _orderRepository.ExportOrdersFilter(statusesParam, categoriesParam, searchtermParam, pageNumberParam, pageSizeParam);
                if (md != null)
                {
                    return File(md, "application/octet-stream", "Order.xlsx");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Page();
        }
    }
}
