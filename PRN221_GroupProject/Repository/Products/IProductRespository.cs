using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Products
{
    public interface IProductRepository
    {
        public void Create(Product product, string userId);
        public void Update(Product product);
        public void Disable(string productId);
        public void Enable(string productId);
        public Product GetProductByID(string productId);

        public Product GetProductByIDInclude(string productId);

    }
}
