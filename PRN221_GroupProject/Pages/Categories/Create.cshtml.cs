using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
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
            var userId = _userManager.GetUserId(User);
            _categoryRepository.Create(Category, userId);


            return RedirectToPage("./Index");
        }
    }
}
