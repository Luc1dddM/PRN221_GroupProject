using PRN221_GroupProject.Models;
using PRN221_GroupProject.DTO;

namespace PRN221_GroupProject.Repository.ProductCategories
{
        public interface IProductCategorieRepository
        {
                public void CreateProductCategories(List<string> categorisId, string color, string productId, int quantity, bool status, string user);
                public void CreateProductCategory(ProductCategory productCategory, string user);
                public void CreateProductCategories(ProductCategory productCategory, List<string> categorisId, string user);
                public void UpdateProductCategories(ProductCategory productCategory, string user);
                public void DisableByCategory(string CategoryId, string user);
                public void EnableByCategory(string CategoryId, string user);
                public void DisableByProduct(string ProductId, string user);
                public void EnableByProduct(string ProductId, string user);
                public void DeleteProductCategory(string ProductId, string CategoryId);
                public List<ProductCategory> GetProductCategoriesByProductID(string ProductId);
                public List<ProductCategory> GetProductCategoriesByCategoryID(string CategoryId);
                public ProductCategory GetProductCategoriesByCategoryAndProductID(string CategoryId, string ProductId);

        }
}
