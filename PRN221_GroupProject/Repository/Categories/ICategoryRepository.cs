using PRN221_GroupProject.DTO;
using PRN221_GroupProject.DTO.category;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Categories
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public List<Category> GetColors(Product Product);
        public List<Category> GetBrands();
        public List<Category> GetDevices();
        public List<Category> GetColors();
        public List<Category> GetChoosedColors(Product Product);
        public Category GetCategoryByID(string categoryId);
        public List<Category> GetDevicesByProduct(Product Product);
        public List<Category> GetBrandsByProduct(Product Product);
        public List<Category> GetChoosedCategoriesByProduct(Product Product);
        public CategoryListDTO GetList(string[] statusesParam, string[] TypeParam, string searchterm, int pageNumberParam, int pageSizeParam);

        public void Create(Category category, string user);
        public void update(Category category, string user);

        public bool haveDevice(Product Product);
        public bool haveBrand(Product Product);

    }
}
