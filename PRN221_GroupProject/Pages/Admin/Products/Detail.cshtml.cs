using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Products;
using PRN221_GroupProject.Repository.File;
using Microsoft.AspNetCore.Identity;

namespace PRN221_GroupProject.Pages.Products
{
    public class DetailModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public IFileUploadRepository _fileUploadRepository;
        public IProductRepository _ProductRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public DetailModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context,
            IProductRepository ProductRepository,
            IFileUploadRepository fileUploadRepository,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _ProductRepository = ProductRepository;
            _fileUploadRepository = fileUploadRepository;
            _userManager = userManager;
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
            try
            {

                if (Imgfile != null)
                {
                    Product.ImageUrl = Imgfile.FileName;
                    _fileUploadRepository.UploadFile(Imgfile);
                }
                _ProductRepository.Update(Product, _userManager.GetUserId(User));
                TempData["success"] = "Update Product successfully";

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }



            return RedirectToPage("./Index");
        }


    }
}
