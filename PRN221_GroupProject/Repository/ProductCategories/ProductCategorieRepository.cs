using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.ProductCategories
{
    public class ProductCategorieRepository : IProductCategorieRepository
    {
        private readonly Prn221GroupProjectContext _dbContext;
        public ProductCategorieRepository(Prn221GroupProjectContext Context)
        {
            _dbContext = Context;
        }

        public void CreateProductCategories(List<string> categorisId, string color, string productId, int quantity, bool status)
        {
            try
            {
                for (int i = 0; i < categorisId.Count; i++)
                {
                    var productCategory = new ProductCategory()
                    {
                        CategoryId = categorisId[i],
                        ProductId = productId,
                        Quantity = 0,
                        Status = status
                    };
                    _dbContext.ProductCategories.Add(productCategory);
                    _dbContext.SaveChanges();

                }

                var _productCategory = new ProductCategory()
                {
                    CategoryId = color,
                    ProductId = productId,
                    Quantity = quantity,
                    Status = status
                };
                _dbContext.ProductCategories.Add(_productCategory);
                _dbContext.SaveChanges();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateProductCategories(ProductCategory productCategory, List<string> categorisId)
        {
            try
            {
                for (int i = 0; i < categorisId.Count; i++)
                {
                    var ProductCategory = new ProductCategory()
                    {
                        CategoryId = categorisId[i],
                        ProductId = productCategory.ProductId,
                        Quantity = 0,
                        Status = true
                    };
                    _dbContext.ProductCategories.Add(ProductCategory);
                    _dbContext.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateProductCategory(ProductCategory productCategory)
        {
            try
            {
                _dbContext.ProductCategories.Add(productCategory);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProductCategory(string ProductId, string CategoryId)
        {
            try
            {
                var productCategory = _dbContext.ProductCategories.FirstOrDefault(c => c.ProductId.Equals(ProductId) && c.CategoryId.Equals(CategoryId));
                _dbContext.ProductCategories.Remove(productCategory);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DisableByCategory(string CategoryId)
        {
            try
            {
                List<ProductCategory> productCategories = GetProductCategoriesByCategoryID(CategoryId);
                if(productCategories.Count != 0)
                {
                    foreach (var productCategory in productCategories)
                    {
                        productCategory.Status = false;
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DisableByProduct(string ProductId)
        {
            try
            {
                List<ProductCategory> productCategories = GetProductCategoriesByProductID(ProductId);
                if (productCategories.Count != 0)
                {
                    foreach (var productCategory in productCategories)
                    {
                        productCategory.Status = false;
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EnableByCategory(string CategoryId)
        {
            try
            {
                List<ProductCategory> productCategories = GetProductCategoriesByCategoryID(CategoryId);
                if (productCategories.Count != 0)
                {
                    foreach (var productCategory in productCategories)
                    {
                        productCategory.Status = true;
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EnableByProduct(string ProductId)
        {
            {
                try
                {
                    List<ProductCategory> productCategories = GetProductCategoriesByProductID(ProductId);
                    if (productCategories.Count != 0)
                    {
                        foreach (var productCategory in productCategories)
                        {
                            productCategory.Status = true;
                            _dbContext.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public ProductCategory GetProductCategoriesByCategoryAndProductID(string CategoryId, string ProductId)
        {
            try
            {
                return _dbContext.ProductCategories.FirstOrDefault(c => c.CategoryId.Equals(CategoryId) && c.ProductId.Equals(ProductId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProductCategory> GetProductCategoriesByCategoryID(string CategoryId)
        {
            try
            {
                return _dbContext.ProductCategories.Where(c => c.CategoryId.Equals(CategoryId)).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProductCategory> GetProductCategoriesByProductID(string ProductId)
        {
            try
            {
                return _dbContext.ProductCategories.Where(c => c.ProductId.Equals(ProductId) && c.Category.Type.Equals("Color")).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProductCategories(ProductCategory productCategory)
        {
            try
            {
                ProductCategory newProductCategory = GetProductCategoriesByCategoryAndProductID(productCategory.CategoryId,productCategory.ProductId);
                newProductCategory.Quantity = productCategory.Quantity;
                newProductCategory.Status = productCategory.Status;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
