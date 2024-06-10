using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.DTO.category;
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

        public void Create(Category category, string user)
        {
            try
            {
                category.CreatedBy = user;
                category.UpdatedBy = user;
                category.UpdatedAt = DateTime.Now;
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

        public void update(Category category, string user)
        {
            try
            {
                var newCategory = GetCategoryByID(category.CategoryId);
                newCategory.Name = category.Name;
                newCategory.Type = category.Type;
                newCategory.UpdatedBy = user;
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
                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => c.Type.Equals("Color")).ToList();
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

        public List<Category> GetBrands()
        {
            try
            {
                return _dbContext.Categories.Where(c => c.Type.Equals("Brand") && c.Status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetDevices()
        {
            try
            {
                return _dbContext.Categories.Where(c => c.Type.Equals("Device") && c.Status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetColors()
        {
            try
            {
                return _dbContext.Categories.Where(c => c.Type.Equals("Color") && c.Status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CategoryListDTO GetList(string[] statusesParam, string[] TypeParam, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _dbContext.Categories.ToList();

            //Call filter function 
            result = Filter(statusesParam, TypeParam, result);
            result = Search(result, searchterm);

            //Calculate pagination
            var totalItems = result.Count();
            var TotalPages = (int)Math.Ceiling((double)totalItems / pageSizeParam);

            //Get final result base on page size and page number 
            result = result.OrderByDescending(e => e.Id)
                    .Skip((pageNumberParam - 1) * pageSizeParam)
                    .Take(pageSizeParam)
                    .ToList();

            return new CategoryListDTO()
            {
                listCategory = result,
                totalPages = TotalPages
            };
        }

        private List<Category> Filter(string[] statuses, string[] types, List<Category> list)
        {
            if (types != null && types.Length > 0)
            {
                list = list.Where(e => types.Contains(e.Type)).ToList();
            }

            if (statuses != null && statuses.Length > 0)
            {
                list = list.Where(e => statuses.Contains(e.Status.ToString())).ToList();
            }

            return list;
        }

        private List<Category> Search(List<Category> list, string searchtearm)
        {
            if (!string.IsNullOrEmpty(searchtearm))
            {
                list = list.Where(p =>
                            p.Name.Contains(searchtearm, StringComparison.OrdinalIgnoreCase))
                            .ToList();
            }
            return list;
        }


    }
}
