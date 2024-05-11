using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.ProductCategories;

namespace PRN221_GroupProject.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public IProductCategorieRepository _ProductCategorieRepository;

        public CreateModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, IProductCategorieRepository ProductCategorieRepository)
        {
            _context = context;
            _ProductCategorieRepository = ProductCategorieRepository;
        }





        [BindProperty]
        public Product Product { get; set; } = default!;
        public IList<Category> Category { get; set; } = default!;
        public IList<Category> CategoryForColors { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public List<string> categories { get; set; } = default!;
        public string colors { get; set; } = default!;


        public async Task<IActionResult> OnGet()
        {
            Category = await _context.Categories.Where(c => c.Type != "Color").ToListAsync();

            CategoryForColors = await _context.Categories.Where(c => c.Type == "Color").ToListAsync();

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            try
            {

                Product.CreatedAt = DateTime.Now;
                Product.CreatedBy = "unknow";
                Product.UpdatedAt = DateTime.Now;
                Product.UpdatedBy = "unknow";
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();

                categories = Request.Form["categories"].ToList();
                colors = Request.Form["color"].ToString();
                Quantity = int.Parse(Request.Form["quantity"]);
                _ProductCategorieRepository.CreateProductCategories(categories, colors, Product.ProductId, Quantity, Product.Status);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }


            return RedirectToPage("./Index");
        }
    }
}
