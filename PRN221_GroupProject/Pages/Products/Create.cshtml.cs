using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.File;
using PRN221_GroupProject.Repository.ProductCategories;
using PRN221_GroupProject.Repository.Products;


namespace PRN221_GroupProject.Pages.Products
{
    [Authorize(Policy = "admin")]
    public class CreateModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public IProductCategorieRepository _ProductCategorieRepository;
        public IProductRepository _ProductRepository;
        public IFileUploadRepository _fileUploadRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, 
            IProductCategorieRepository ProductCategorieRepository, 
            IProductRepository ProductRepository,
            IFileUploadRepository fileUploadRepository,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _ProductCategorieRepository = ProductCategorieRepository;
            _ProductRepository = ProductRepository;
            _fileUploadRepository = fileUploadRepository;
            _userManager = userManager;
        }





        [BindProperty]
        public Product Product { get; set; } = default!;
        public IList<Category> Category { get; set; } = default!;
        public IList<Category> CategoryForColors { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public List<string> categories { get; set; } = default!;
        public string colors { get; set; } = default!;
        public IFormFile ProductImg { get; set; } = default!;


        public async Task<IActionResult> OnGet()
        {
            Category = await _context.Categories.Where(c => c.Type != "Color").ToListAsync();

            CategoryForColors = await _context.Categories.Where(c => c.Type == "Color").ToListAsync();

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile Imgfile)
        {

            try
            {
                string userId = _userManager.GetUserId(User);
                categories = Request.Form["categories"].ToList();
                colors = Request.Form["color"].ToString();
                Quantity = int.Parse(Request.Form["quantity"]);
                Product.ImageUrl = Imgfile.FileName;

                _ProductRepository.Create(Product, userId);
                _fileUploadRepository.UploadFile(Imgfile);
                _ProductCategorieRepository.CreateProductCategories(categories, colors, Product.ProductId, Quantity, Product.Status, userId);
                

            }
            catch (Exception ex)
            {

            }


            return RedirectToPage("./Index");
        }
    }
}
