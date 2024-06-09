using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Categories
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public List<Category> GetColors(Product Product);
        public List<Category> GetChoosedColors(Product Product);

        public List<Category> GetCategoriesByProduct(Product Product);
        public List<Category> GetChoosedCategoriesByProduct(Product Product);

        public void Create(Category category, string userId);
        public void update(Category category);
        public Category GetCategoryByID(string categoryId);
    }
}
