namespace PRN221_GroupProject.Repository.ProductCategories
{
    public interface IProductCategorieRepository
    {
        public void CreateProductCategories(List<string> categorisId,string color, string productId, int quantity, bool status);
    }
}
