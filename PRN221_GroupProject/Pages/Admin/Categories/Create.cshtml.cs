using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;

namespace PRN221_GroupProject.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public ICategoryRepository _categoryRepository;

        public CreateModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*            if (!ModelState.IsValid)
                        {
                            return Page();
                        }*/
            _categoryRepository.Create(Category);


            return RedirectToPage("./Index");
        }
    }
}
