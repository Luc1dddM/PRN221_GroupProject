using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.ProductCategories
{
    public interface IProductCategorieRepository
    {
        public void CreateProductCategories(List<string> categorisId,string color, string productId, int quantity, bool status, string userId);
        public void CreateProductCategory(ProductCategory productCategory);
        public void CreateProductCategories(ProductCategory productCategory, List<string> categorisId);
        public void UpdateProductCategories(ProductCategory productCategory);

        public void DisableByCategory(string CategoryId);
        public void EnableByCategory(string CategoryId);
        public void DisableByProduct(string ProductId);
        public void EnableByProduct(string ProductId);
        public void DeleteProductCategory(string ProductId, string CategoryId);
        public List<ProductCategory> GetProductCategoriesByProductID(string ProductId);
        public List<ProductCategory> GetProductCategoriesByCategoryID(string CategoryId);
        public ProductCategory GetProductCategoriesByCategoryAndProductID(string CategoryId, string ProductId);

    }
}
