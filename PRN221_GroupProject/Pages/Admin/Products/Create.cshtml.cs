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
using PRN221_GroupProject.DTO.product;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.File;
using PRN221_GroupProject.Repository.ProductCategories;
using PRN221_GroupProject.Repository.Products;
using PRN221_GroupProject.Repository.Users;


namespace PRN221_GroupProject.Pages.Products
{
    [Authorize(Policy = "admin")]
    public class CreateModel : PageModel
    {
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public IProductCategorieRepository _ProductCategorieRepository;
        public IProductRepository _ProductRepository;
        public ICategoryRepository _categoryRepository;
        public IUserRepository _userRepository;
        public IFileUploadRepository _fileUploadRepository;

        public CreateModel(PRN221_GroupProject.Models.Prn221GroupProjectContext context,
            IProductCategorieRepository ProductCategorieRepository,
            IProductRepository ProductRepository,
            IFileUploadRepository fileUploadRepository,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _ProductCategorieRepository = ProductCategorieRepository;
            _ProductRepository = ProductRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _fileUploadRepository = fileUploadRepository;
            _userManager = userManager;
        }





        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public IFormFile ProductImg { get; set; } = default!;
        public IList<Category> Devices { get; set; } = default!;
        public IList<Category> Brands { get; set; } = default!;
        public IList<Category> Colors { get; set; } = default!;
        [BindProperty]
        public int Quantity { get; set; } = default!;
        [BindProperty]
        public string brand { get; set; } = default!;
        [BindProperty]
        public string device { get; set; } = default!;
        [BindProperty]
        public string color { get; set; } = default!;



        public IActionResult OnGet()
        {

            Brands = _categoryRepository.GetBrands();
            Devices = _categoryRepository.GetDevices();
            Colors = _categoryRepository.GetColors();

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {

            try
            {
                if (Product.Name == null || ProductImg == null ||
                    Product.Price == null || Product.Description == null || Quantity == null)
                {
                    if (Product.Name == null)
                    {
                        ModelState.AddModelError("Product.Name", "The field name can not be null!");
                    }
                    if (ProductImg == null)
                    {
                        ModelState.AddModelError("ProductImg", "The field image can not be null!");
                    }
                    if (Product.Price == null)
                    {
                        ModelState.AddModelError("Product.Price", "The field price can not be null!");
                    }
                    if (Product.Description == null)
                    {
                        ModelState.AddModelError("Product.Description", "The field description can not be null!");
                    }
                    if (Quantity == null)
                    {
                        ModelState.AddModelError("Quantity", "The field quantity can not be null!");
                    }
                    Brands = _categoryRepository.GetBrands();
                    Devices = _categoryRepository.GetDevices();
                    Colors = _categoryRepository.GetColors();
                    return Page();
                }
                if (Product.Price < 0 || Quantity < 0)
                {

                    if (Product.Price < 0)
                    {
                        ModelState.AddModelError("Product.Price", "The price must be greater than or equal to 0!");
                    }
                    if (Product.Description == null)

                    if (Quantity < 0)
                    {
                        ModelState.AddModelError("Quantity", "The quantity must be greater than or equal to 0!");
                    }
                    Brands = _categoryRepository.GetBrands();
                    Devices = _categoryRepository.GetDevices();
                    Colors = _categoryRepository.GetColors();
                    return Page();
                }


                /*                categories = Request.Form["categories"].ToList();
                                colors = Request.Form["color"].ToString();
                                Quantity = int.Parse(Request.Form["quantity"]);*/
                var userId = _userManager.GetUserId(User);
                Product.ImageUrl = _fileUploadRepository.UploadFile(ProductImg);
                _ProductRepository.Create(Product, userId);

                _ProductCategorieRepository.CreateProductCategories(brand, device, color, Product.ProductId, Quantity, Product.Status, userId);
                TempData["success"] = "Create product successfully";


            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }


            return RedirectToPage("./Index");
        }
    }
}
