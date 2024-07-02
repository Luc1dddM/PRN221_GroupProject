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
                var userId = _userManager.GetUserId(User);
                _repository.Update(Coupon, userId);

                TempData["success"] = "Coupon updated successfully";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Error: {ex.Message}";
            }

            return RedirectToPage("./Index");
        }

    }
}
