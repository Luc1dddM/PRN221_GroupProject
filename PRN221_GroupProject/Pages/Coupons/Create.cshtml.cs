using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Pages.Coupons
{
    public class CreateModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;

        public CreateModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context)
        {
            _context = context;
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
                _context.Coupons.Add(Coupon);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch
            {
                return Page();
            }


            /* if (!ModelState.IsValid)
             {
                 return Page();
             }
             _context.Coupons.Add(Coupon);
             await _context.SaveChangesAsync();

             return RedirectToPage("./Index");*/
        }
    }
}
