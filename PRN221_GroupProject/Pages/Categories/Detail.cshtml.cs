using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using Syncfusion.EJ2.Linq;

namespace PRN221_GroupProject.Pages.Categories
{
    public class DetailModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;

        public DetailModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;
        public IList<ProductCategory> ProductCategories { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(string? Categoryid)
        {
            if (Categoryid == null)
            {
                return NotFound();
            }

            

            var category =  await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId.Equals(Categoryid));
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                ProductCategories = await _context.ProductCategories.Where(c => c.CategoryId.Equals(Category.CategoryId)).ToListAsync();
                if(ProductCategories.Count() != 0)
                {
                    ProductCategories.ForEach(c => c.Status = Category.Status);
                    _context.Attach(ProductCategories).State = EntityState.Modified;
                }

                _context.Attach(Category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.CategoryId))
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

        private bool CategoryExists(string Categoryid)
        {
            return _context.Categories.Any(e => e.CategoryId.Equals(Categoryid));
        }
    }
}
