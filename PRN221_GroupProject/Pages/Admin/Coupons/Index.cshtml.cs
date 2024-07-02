using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Coupons;

namespace PRN221_GroupProject.Pages.Coupons
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly ICouponRepository _couponRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ICouponRepository couponRepository, UserManager<ApplicationUser> userManager)
        {
            _couponRepository = couponRepository;
            _userManager = userManager;
        }

        public IList<Coupon> Coupon { get; set; } = default!;
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public string searchterm { get; set; }
        public string[] statuses { get; set; }

        public double minAmount { get; set; }

        public double maxAmount { get; set; }

        public string sortBy { get; set; }
        public string sortOrder { get; set; }
        public string currentSortBy { get; set; }


        public IActionResult OnGetAsync(string[] statusesParam, double? minAmountParam, double? maxAmountParam, bool keepSort = false, string currentSortByParam = "", string searchtermParam = "", string sortByParam = "", string sortOrderParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            statuses = statusesParam;
            minAmount = minAmountParam ?? 0;
            maxAmount = maxAmountParam ?? 0;
            searchterm = searchtermParam;

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
                    else //desc => bỏ sort
                    {
                        sortOrderParam = "";
                        sortByParam = "";
                    }
                }
            }

            sortBy = sortByParam;
            sortOrder = sortOrderParam;

            // thêm cái này để so sánh 2 cái sort order cũ vs mới
            currentSortBy = sortByParam;

            var couponPagination = _couponRepository.GetList(statusesParam, minAmountParam, maxAmountParam, searchtermParam, sortByParam, sortOrderParam, pageNumberParam, pageSizeParam);

            Coupon = couponPagination.listCoupon;
            TotalPages = couponPagination.totalPages;
           


            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize, minAmount = minAmountParam, maxAmount = maxAmountParam });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUploadExcel(IFormFile excelFile)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (excelFile != null && excelFile.Length > 0)
                {
                    await _couponRepository.ImportCoupons(excelFile, _userManager.GetUserId(User));
                    TempData["success"] = "Import coupons successfully";
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
            return Redirect("/admin/coupons");
        }


        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OnGetExportExcel(string[] statusesParam, double? minAmountParam, double? maxAmountParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            statuses = statusesParam;
            minAmount = minAmountParam ?? 0;
            maxAmount = maxAmountParam ?? 0;
            searchterm = searchtermParam;
            try
            {
                var md = await _couponRepository.ExportCouponFilter(statusesParam, minAmountParam, maxAmountParam, searchtermParam, pageNumberParam, pageSizeParam);
                if (md != null)
                {
                    return File(md, "application/octet-stream", "Coupon.xlsx");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Page();

        }
    }

}
