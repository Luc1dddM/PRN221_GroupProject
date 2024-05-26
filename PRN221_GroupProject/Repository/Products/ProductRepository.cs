using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly Prn221GroupProjectContext _dbContext;
        public ProductRepository(Prn221GroupProjectContext Context)
        {
            _dbContext = Context;
        }
        public void Create(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.CreatedBy = "unknow";
            product.UpdatedAt = DateTime.Now;
            product.UpdatedBy = "unknow";
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Disable(string productId)
        {
            try
            {
                Product product = GetProductByID(productId);
                product.Status = false;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Enable(string productId)
        {
            try
            {
                Product product = GetProductByID(productId);
                product.Status = true;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Product GetProductByID(string productId)
        {
            try
            {
                return _dbContext.Products.FirstOrDefault(p => p.ProductId.Equals(productId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product GetProductByIDInclude(string productId)
        {
            try
            {
                return _dbContext.Products.Include(p => p.ProductCategories).FirstOrDefault(p => p.ProductId.Equals(productId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                Product newProduct = GetProductByID(product.ProductId);
                newProduct.Id = product.Id;
                newProduct.ProductId = product.ProductId;
                newProduct.Name = product.Name;
                newProduct.Description = product.Description;
                newProduct.Price = product.Price;
                newProduct.ImageUrl = product.ImageUrl;
                newProduct.Status = product.Status;
                newProduct.CreatedAt = product.CreatedAt;
                newProduct.CreatedBy = product.CreatedBy;
                newProduct.UpdatedBy = "unknow";
                newProduct.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
