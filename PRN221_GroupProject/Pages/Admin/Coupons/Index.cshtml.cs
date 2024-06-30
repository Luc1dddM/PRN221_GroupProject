using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IndexModel(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public IList<Coupon> Coupon { get; set; } = default!;
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public string searchterm { get; set; }
        public string[] statuses { get; set; }

        public double minAmount { get; set; }

        public double maxAmount { get; set; }


        public IActionResult OnGetAsync(string[] statusesParam, double? minAmountParam, double? maxAmountParam, string searchtermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            pageSize = pageSizeParam;
            pageNumber = pageNumberParam;
            statuses = statusesParam;
            minAmount = minAmountParam ?? 0;
            maxAmount = maxAmountParam ?? 0;

            var couponPagination = _couponRepository.GetList(statusesParam, minAmountParam, maxAmountParam, searchtermParam, pageNumberParam, pageSizeParam);

            Coupon = couponPagination.listCoupon;
            TotalPages = couponPagination.totalPages;
            searchterm = searchtermParam;


            if (pageNumber < 1 || (pageNumber > TotalPages && TotalPages > 0))
            {
                return RedirectToPage(new { pageNumber = 1, pageSize = pageSize });
            }

            return Page();
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
