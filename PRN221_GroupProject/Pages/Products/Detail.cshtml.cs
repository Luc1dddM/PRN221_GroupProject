using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using Newtonsoft.Json;
using PRN221_GroupProject.Repository.ProductCategories;
using PRN221_GroupProject.Repository.Products;
using PRN221_GroupProject.Repository.File;

namespace PRN221_GroupProject.Pages.Products
{
    public class DetailModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public IFileUploadRepository _fileUploadRepository;
        public IProductRepository _ProductRepository;

        public DetailModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context, 
            IProductRepository ProductRepository, 
            IFileUploadRepository fileUploadRepository)
        {
            _context = context;
            _ProductRepository = ProductRepository;
            _fileUploadRepository = fileUploadRepository;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public IFormFile ProductImg { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(string? ProductId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }

/*            ProductCategories = _ProductCategorieRepository.GetProductCategoriesByProductID(ProductId);
            ViewData["ProductCategoriesJson"] = JsonConvert.SerializeObject(ProductCategories);*/

/*            Category = await _context.Categories.ToListAsync();*/
            
            var product = _ProductRepository.GetProductByID(ProductId);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile Imgfile)
        {


            if (Imgfile != null)
            {
                Product.ImageUrl = Imgfile.FileName;
                _fileUploadRepository.UploadFile(Imgfile);
            }
            _ProductRepository.Update(Product);


            return RedirectToPage("./Index");
        }


    }
}
