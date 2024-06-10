using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Categories
{
    public interface ICategoryRepository
        {
                public List<Category> GetCategories();
                public List<Category> GetColors(Product Product);
                public List<Category> GetBrands();
                public List<Category> GetDevices();
                public List<Category> GetChoosedColors(Product Product);

                public List<Category> GetCategoriesByProduct(Product Product);
                public List<Category> GetChoosedCategoriesByProduct(Product Product);

                public void Create(Category category, string user);
                public void update(Category category, string user);
                public Category GetCategoryByID(string categoryId);
                public CategoryListDTO GetList(string[] statusesParam, string[] TypeParam, string searchterm, int pageNumberParam, int pageSizeParam);
        }
}
