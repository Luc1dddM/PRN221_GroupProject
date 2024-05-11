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

        public void CreateProductCategories(List<string> categorisId,string color, string productId, int quantity, bool status)
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
                    /*_dbContext.SaveChangesAsync();*/

                }

                var _productCategory = new ProductCategory()
                {
                    CategoryId = color,
                    ProductId = productId,
                    Quantity = quantity,
                    Status = status
                };
                _dbContext.ProductCategories.Add(_productCategory);
                /*_dbContext.SaveChangesAsync();*/


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
