using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Orders
{
    public interface IOrderRepository
    {
        public Task CreateOrderHeader(OrderHeader orderHeader, string userId);
        public Task CreateOrderDetail(CartDetail cartDetail, string orderHeaderId);
        public OrderHeader GetOrderHeaderById(string orderHeaderId);
        public OrderHeader GetOrderHeaderByUserId(string userId);
        public OrderDetail GetOrderDetailByOrderHeaderId(string orderHeaderId);
        public OrderListDTO GetList(string[] statusesParam, string[] categoriesParam, string searchterm, int pageNumberParam, int pageSizeParam);
    }
}
