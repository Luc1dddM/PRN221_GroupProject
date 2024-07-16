using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Coupons;

namespace PRN221_GroupProject.Pages.Coupons
{
    [Authorize(Policy = "admin")]
    public class EditModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICouponRepository _repository;



        public EditModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager, ICouponRepository repository)
        {
            _context = context;
            _userManager = userManager;
            _repository = repository;
        }

        [BindProperty]
        public Coupon Coupon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons.FirstOrDefaultAsync(m => m.Id == id);
            if (coupon == null)
            {
                return NotFound();
            }
            Coupon = coupon;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    var regex = new Regex(@"^[a-zA-Z0-9]+$");
                    if (!regex.IsMatch(Coupon.CouponCode))
                    {
                        ModelState.AddModelError("Coupon.CouponCode", "Coupon code must contain only letters and numbers.");
                        return Page();
                    }
                    var existingCoupon = _repository.GetCouponByCode(Coupon.CouponCode);

                    if (existingCoupon != null && existingCoupon.Id != Coupon.Id)
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
                    var couponInDb = await _context.Coupons.AsNoTracking().FirstOrDefaultAsync(m => m.Id == Coupon.Id);
                    if (couponInDb == null)
                    {
                        return NotFound();
                    }
                    if (couponInDb.CouponCode != Coupon.CouponCode)
                    {
                        // Coupon code has changed, check if the new code already exists
                        if (_repository.GetCouponByCode(Coupon.CouponCode) != null)
                        {
                            ModelState.AddModelError("Coupon.CouponCode", $"Coupon code '{Coupon.CouponCode}' already exists.");
                            return Page();
                        }
                    }
                    var userId = _userManager.GetUserId(User);
                    _repository.Update(Coupon, userId);

                    TempData["success"] = "Coupon updated successfully";
                    return RedirectToPage("./Index");
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToPage("./Index");
        }

    }
}
