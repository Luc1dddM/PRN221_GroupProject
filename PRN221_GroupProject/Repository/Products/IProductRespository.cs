using PRN221_GroupProject.DTO;
using PRN221_GroupProject.DTO.product;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Products
{
    public interface IProductRepository
        {
                public void Create(Product product, string user);
                public void Update(Product product, string user);
                public void Disable(string productId, string user);
                public void Enable(string productId, string user);
                public List<Product> GetAll();
                public Product GetProductByID(string productId);

                public Product GetProductByIDInclude(string productId);
                public ProductListDTO GetList(string[] brandParam, string[] deviceParam, string Price1, string Price2, string searchterm, int pageNumberParam, int pageSizeParam);
                public ProductListDTO GetListCustomer(string[] brandParam, string[] deviceParam, string Price1, string Price2, string searchterm, int pageNumberParam, int pageSizeParam);

        }
}
