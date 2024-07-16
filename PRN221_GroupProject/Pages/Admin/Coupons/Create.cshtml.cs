using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Coupons;

namespace PRN221_GroupProject.Pages.Coupons
{
    [Authorize(Policy = "admin")]
    public class CreateModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICouponRepository _repository;

        public CreateModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager, ICouponRepository repository)
        {
            _context = context;
            _userManager = userManager;
            _repository = repository;

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Coupon Coupon { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var existingCoupon = _repository.GetCouponByCode(Coupon.CouponCode);
                    if (existingCoupon != null)
                    {

                        ModelState.AddModelError("Coupon.CouponCode", $"Coupon code '{Coupon.CouponCode}' already exists.");
                        return Page();

                    }
                    if (Coupon.DiscountAmount <= 0)
                    {
                        ModelState.AddModelError("Coupon.DiscountAmount", "Discount amount must be a positive number.");
                        return Page();
                    }
                    if (Coupon.MinAmount < 0)
                    {
                        ModelState.AddModelError("Coupon.MinAmount", "Min amount must be a positive number.");
                        return Page();
                    }

                    if (Coupon.MaxAmount < 0)
                    {
                        ModelState.AddModelError("Coupon.MaxAmount", "Max amount must be a positive number.");
                        return Page();
                    }
                    if (Coupon.MinAmount > Coupon.MaxAmount)
                    {
                        ModelState.AddModelError("Coupon.MinAmount", "Min amount must be less than Max amount.");
                        ModelState.AddModelError("Coupon.MaxAmount", "MaxAmount must be grater than Min amount.");
                        return Page();
                    }
                    var userId = _userManager.GetUserId(User);
                    _repository.Create(Coupon, userId);
                    TempData["success"] = "Create coupon successfully";
                    return RedirectToPage("./Index");

                }


            }
            catch (Exception ex)
            {
              
            }
            return Page();
        }
    }
}
