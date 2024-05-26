using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Products;

namespace PRN221_GroupProject.Repository.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Prn221GroupProjectContext _dbContext;
        public IProductRepository _productsRepository;
        public CategoryRepository(Prn221GroupProjectContext Context, IProductRepository productRepository)
        {
            _dbContext = Context;
            _productsRepository = productRepository;
        }

        public void Create(Category category)
        {
            try
            {
                category.UpdatedBy = "unknow";
                category.UpdatedAt = DateTime.Now;
                category.CreatedBy = "unknow";
                category.CreatedAt = DateTime.Now;
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetCategories()
        {
            try
            {
                return _dbContext.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Category GetCategoryByID(string categoryId)
        {
            try
            {
                return _dbContext.Categories.FirstOrDefault(c => c.CategoryId.Equals(categoryId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void update(Category category)
        {
            try
            {
                var newCategory = GetCategoryByID(category.CategoryId);
                newCategory.Name = category.Name;
                newCategory.Type = category.Type;
                newCategory.UpdatedBy = "Unknow";
                newCategory.UpdatedAt = DateTime.Now;
                newCategory.Status = category.Status;
                _dbContext.SaveChanges();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetColors(Product Product)
        {
            try
            {
                List<ProductCategory> productCategories = Product.ProductCategories.ToList();
                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => c.Type.Equals("Color") ).ToList();
                categories = categories.Where(c => !c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetChoosedColors(Product Product)
        {
            try
            {
                List<ProductCategory> productCategories = Product.ProductCategories.ToList();
                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => c.Type.Equals("Color")).ToList();
                categories = categories.Where(c => c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetCategoriesByProduct(Product Product)
        {
            try
            {
        
                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => !c.Type.Equals("Color")).ToList();
                categories = categories.Where(c => !c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<Category> GetChoosedCategoriesByProduct(Product Product)
        {
            try
            {

                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => !c.Type.Equals("Color")).ToList();
                categories = categories.Where(c => c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
