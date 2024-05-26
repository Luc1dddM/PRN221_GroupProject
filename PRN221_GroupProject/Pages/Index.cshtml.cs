using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Products;

namespace PRN221_GroupProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PRN221_GroupProject.Models.Prn221GroupProjectContext _context;
        public IProductRepository _ProductRepository;

        public IndexModel(ILogger<IndexModel> logger, Models.Prn221GroupProjectContext context, IProductRepository productRepository)
        {
            _logger = logger;
            _context = context;
            _ProductRepository = productRepository;
        }
        public IList<Product> Product { get; set; } = default!;
        public void OnGet()
        {
            Product = _context.Products.Take(12).ToList();

        }
    }
}