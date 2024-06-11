using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Pages.Coupons
{
    [Authorize(Policy = "admin")]
    public class EditModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public EditModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Coupon Coupon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon =  await _context.Coupons.FirstOrDefaultAsync(m => m.Id == id);
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

            var couponToUpdate = await _context.Coupons.FindAsync(Coupon.Id);

            if (couponToUpdate == null)
            {
                return NotFound();
            }

            // Update the fields
            couponToUpdate.CouponCode = Coupon.CouponCode;
            couponToUpdate.DiscountAmount = Coupon.DiscountAmount;
            couponToUpdate.MinAmount = Coupon.MinAmount;
            couponToUpdate.MaxAmount = Coupon.MaxAmount;
            couponToUpdate.Status = Coupon.Status;
            couponToUpdate.UpdatedDate = DateTime.Now;
            couponToUpdate.UpdatedBy = _userManager.GetUserId(User);

            // Attach the updated entity and set its state to modified
            _context.Attach(couponToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["success"] = "Coupon updated successfully";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponExists(Coupon.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CouponExists(int id)
        {
            return _context.Coupons.Any(e => e.Id == id);
        }
    }
}
