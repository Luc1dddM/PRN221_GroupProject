using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Carts
{
    public interface ICartRepository
    {
        public void CreateCartHeader(string userId);
        public void CreateCartDetail(CartDetail cartDetail, string userId);
        public void UpdateCartDetailQuantity(CartDetail cartDetail, string userId);
        public void UpdateCartDetailQuantityByInputField(CartDetail cartDetail, string userId);
        public void UpdateCartDetailColor(CartDetail cartDetail, string userId);
        public void DeleteCartDetail(CartDetail cartDetail, string userId);
        public CartHeader GetCartHeaderByUserId(string userId);
        public CartDetail GetCartDetailById(string cartDetailId);
        public CartDetail GetCartDetailByCartId_ProId(string cartHeaderId, string productId, string color);
        public CartDetail GetCartDetailByCartHeaderId_ProductId(string cartHeaderId, string productId);
        public IList<CartDetail> GetCartDetailsByUserId(string userId);

    }
}
